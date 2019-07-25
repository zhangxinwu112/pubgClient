using AppClient;
using server.Utils;
using SuperSocket.ClientEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using Tool;
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
    private int mHeartBeatInterval = 1000 * 5;
    private int mReConnectionInterval = 1000 * 5;

   
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
       tmrHeartBeat = new System.Threading.Timer(HeartBeatCallBack, null, mHeartBeatInterval, mHeartBeatInterval);
       tmrReConnection = new System.Threading.Timer(ReConnectionCallBack, null, mReConnectionInterval, mReConnectionInterval);
    }

   
    private void HeartBeatCallBack(object state)
    {
        try
        {
            tmrHeartBeat.Change(Timeout.Infinite, Timeout.Infinite);
            if (client != null && client.IsConnected)
            {
                string sendData = "HeartBeat" + Constant.START_SPLIT;
                Send(sendData);
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
            StopTimer();
        }
    }

    ~PubgSocket() // 析构函数
    {
        Close();
        StopTimer();
    }

    private void StopTimer()

    {
        if(tmrHeartBeat!=null)
        {
            tmrHeartBeat.Dispose();
        }

        if(tmrReConnection != null)
        {
            tmrReConnection.Dispose();
        }
    }
}
