﻿using AppClient;
using server.Utils;
using SuperSocket.ClientEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

public sealed class PubgSocket  {


    //public  readonly string IP = "39.106.190.144";

    private int port = 9000;

    private EasyClient client = null;
    /// <summary>
    /// 心跳检查定时器
    /// </summary>
    private System.Threading.Timer tmrHeartBeat = null;
    /// <summary>
    /// 断线重连定时器
    /// </summary>
    private System.Threading.Timer tmrReConnection = null;
    private int mHeartBeatInterval = 1000 * 10;
    private int mReConnectionInterval = 1000 * 10;

    //得到
    private System.Threading.Timer getPositionTimer = null;
    private int mgetPositionInterval = 1000 * 10;


    private ISyncManager syncManager;
    public System.Action<string,string,string[]> callBack;
    public void InitTimer(ISyncManager sm)
    {
        this.syncManager = sm;
        Timer();
    }
    public async  void  Init()
    {
         client = new EasyClient();
        client.Initialize(new MyTerminatorReceiveFilter(), (request) => {
            // handle the received request
            syncManager.Add(() =>
            {
                callBack(request.Key, request.Body, request.Parameters);
            });
           // Debug.Log(request.Body);
            //NGUIDebug.Log(request.Body);
            
        });

        client.Error += (s, e) =>
        {
            NGUIDebug.Log(e.Exception.Message);
        };

        client.Closed += (s, e) => {


        };


        //   client.NewPackageReceived += Client_NewPackageReceived;

        string IP = Config.parse("ServerIP");
         var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(IP), port));

        if (connected)
        {
           // NGUIDebug.Log("connet success");
             Debug.Log("connet success");
            // Send data to the server
            //client.Send(Encoding.ASCII.GetBytes("ADD*1#2 \r\n"));
           // Send("ADD*1#2");


        }
    }

    public void Send(string sendContent)
    {
        
        if (client!=null)
        {
            sendContent += "\r\n";
            var data = Encoding.UTF8.GetBytes(sendContent.ToString());
            client.Send(new ArraySegment<byte>(data, 0, data.Length));
           // client.Send(Encoding.ASCII.GetBytes("ADD*1#2 \r\n"));
        }
    }

    private void Timer()
    {
       // tmrHeartBeat = new System.Threading.Timer(HeartBeatCallBack, null, mHeartBeatInterval, mHeartBeatInterval);
        tmrReConnection = new System.Threading.Timer(ReConnectionCallBack, null, mReConnectionInterval, mReConnectionInterval);
       // getPositionTimer = new System.Threading.Timer(GetPostionTimer, null, mgetPositionInterval, mgetPositionInterval);
    }

    /// <summary>
    /// 获取位置
    /// </summary>
    /// <param name="state"></param>
    //private void GetPostionTimer(object state)
    //{
    //    if(Input.location.isEnabledByUser)
    //    {
    //        float lat = Input.location.lastData.latitude;
    //        float lon = Input.location.lastData.longitude;
    //       double[] datas =   GPSTools.gps84_To_Gcj02(lat, lon);
    //       double _lat = datas[0];
    //       double _lon = datas[1];
    //        if(ShowMapPoint.instacne!=null)
    //        {
    //            ShowMapPoint.instacne.Show(_lon, _lat);
    //        }
    //    }
    //}
    private void HeartBeatCallBack(object state)
    {
        try
        {
            tmrHeartBeat.Change(Timeout.Infinite, Timeout.Infinite);
            if (client != null && client.IsConnected)
            {
                var sbMessage = new StringBuilder();
                //sbMessage.AppendFormat(string.Format("heartbeat #{0}#\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")));
                sbMessage.AppendFormat(string.Format("heartbeat #{0}#\r\n", "心跳数据包:ok"));
                var data = Encoding.UTF8.GetBytes(sbMessage.ToString());
                client.Send(new ArraySegment<byte>(data, 0, data.Length));
            }
        }
        finally
        {
            tmrHeartBeat.Change(mHeartBeatInterval, mHeartBeatInterval);
        }
    }

    private void ReConnectionCallBack(object state)
    {
        try
        {
            tmrReConnection.Change(Timeout.Infinite, Timeout.Infinite);
            if (client != null &&
                client.IsConnected == false)
            {
                Init();
            }
        }
        finally
        {
            tmrReConnection.Change(mHeartBeatInterval, mHeartBeatInterval);
        }
    }

    public void Close()
    {
        if(client!=null)
        {
            client.Close();
        }
    }

    ~PubgSocket() // 析构函数
    {
        Close();
    }
}
