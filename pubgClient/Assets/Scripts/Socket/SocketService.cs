using Br.Core.Server;
using command;
using Core.Server.Command;
using server;
using server.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class SocketService : MonoBehaviour
{

    private PubgSocket pubgSocket;

    public static SocketService instance;
    private ISyncManager sm;

    private Action<Action> processCallback = (fun) =>
  {
      fun();
  };

    private CommandsUtils commandsUtils;

    private void Init()
    {
        commandsUtils = new CommandsUtils();
        List<ICommand> loaderList = CommandLoader.Load();
        commandsUtils.RegCommands(loaderList);
    }


    private PostEngine postEngine;

    void Start()
    {
        instance = this;
        Init();
        sm = new SyncManager(processCallback);
        pubgSocket = new PubgSocket();
        pubgSocket.callBack = ReceiveData;
        pubgSocket.Init();
        pubgSocket.InitTimer(sm);
        postEngine = new PostEngine();
        postEngine.RegisterEvent();
    }

    public void ReceiveData(string key, string body, string[] paraters)
    {
       // Debug.Log(key + "," + body);
        // NGUIDebug.Log(key + ","+ body);
        commandsUtils.Exec(key, body, paraters);
    }

    private void OnDestroy()
    {
        if (pubgSocket != null)
        {
            pubgSocket.Close();
        }
    }


    public void SendData(string data)
    {
        pubgSocket.Send(data);
    }
    public void PostData(string method, string[] parameter, System.Action<string> callBack)
    {
        postEngine.PostData(method, parameter, callBack);
    }
    private void Update()
    {
        if (sm != null)
        {

        }
        sm.Exec();

    
        //string sendData = CommandName.ECHO.ToString() + Constant.START_SPLIT + "update里";
        //SendData(sendData);
    }

    private void OnApplicationPause(bool focus)
    {
        //string sendData = "";
        //if (focus)   //进入程序状态更改为前台
        //{
        //   sendData = CommandName.ECHO.ToString() + Constant.START_SPLIT + "前台运行";
        //}
        //else
        //{
        //    sendData = CommandName.ECHO.ToString() + Constant.START_SPLIT + "后台运行";

        //}
        //SendData(sendData);

    }

    private void  OnApplicationFocus(bool focus)
    {

        //string sendData = "";
        //if (focus)   //进入程序状态更改为前台
        //{
        //    sendData = CommandName.ECHO.ToString() + Constant.START_SPLIT + "得到焦点";
        //}
        //else
        //{
        //    sendData = CommandName.ECHO.ToString() + Constant.START_SPLIT + "失去焦点";

        //}
        //SendData(sendData);
    }
}
