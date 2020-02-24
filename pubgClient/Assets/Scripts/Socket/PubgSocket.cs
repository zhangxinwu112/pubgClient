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
    private int mHeartBeatInterval = 1000 * 3;
    /// 心跳检查定时器
    private System.Threading.Timer tmrHeartBeat = null;

    private int mReConnectionInterval = 1000 * 1;
    /// 断线重连定时器
    private System.Threading.Timer tmrReConnection = null;

    private int mReLoginInterval = 1000 * 3;
    /// 登录发送定时器
    private System.Threading.Timer tmrReLogin = null;

    private ISyncManager syncManager;
    public System.Action<string,string,string[]> callBack;
    public void InitTimer(ISyncManager sm)
    {
        this.syncManager = sm;
        Timer();
    }

    //是否已经登陆
    private bool isLogin = false;
    public bool IsLogin
    {
        get
        {
            return isLogin;
        }
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
            //  NGUIDebug.Log(e.Exception.Message);
            isLogin = false;
        };

        client.Closed += (s, e) => {

            isLogin = false;
        };


        //   client.NewPackageReceived += Client_NewPackageReceived;

        string IP = Config.parse("ServerIP");
         var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(IP), port));

        if (connected)
        {
           // NGUIDebug.Log("connet success");
             Debug.Log("connet success");
            isLogin = true;
            // Send data to the server
            //client.Send(Encoding.ASCII.GetBytes("ADD*1#2 \r\n"));
            // Send("ADD*1#2");


        }
    }

    public void Send(string sendContent)
    {
        
        if (client!=null&& client.IsConnected)
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
       tmrReLogin = new System.Threading.Timer(ReLoginCallBack, null, mReLoginInterval, mReLoginInterval);
        
    }

   
    /// <summary>
    /// 心跳
    /// </summary>
    /// <param name="state"></param>
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

    /// <summary>
    /// 重连
    /// </summary>
    /// <param name="state"></param>
    private void ReConnectionCallBack(object state)
    {
        try
        {
            tmrReConnection.Change(Timeout.Infinite, Timeout.Infinite);
            if ((client != null && client.IsConnected == false) || !isLogin)
            {
                Init();
            }
        }
        finally
        {
            tmrReConnection.Change(mHeartBeatInterval, mHeartBeatInterval);
        }
    }

    private void ReLoginCallBack(object state)
    {
        try
        {
            tmrReLogin.Change(Timeout.Infinite, Timeout.Infinite);

            //已登录
            if(LoginInfo.Userinfo!=null)
            {
                string sendData = command.CommandName.RequestLogin.ToString() + Constant.START_SPLIT + LoginInfo.Userinfo.id;
                Send(sendData);
            }
            else
            {
                string sendData = command.CommandName.RequestLogin.ToString() + Constant.START_SPLIT + "-1";
                Send(sendData);
            }
        }
        finally
        {
            tmrReLogin.Change(mReLoginInterval, mReLoginInterval);
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

    public void CloseSocket()
    {
        if (client != null)
        {
            client.Close();
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
