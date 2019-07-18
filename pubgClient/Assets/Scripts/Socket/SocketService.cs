using Br.Core.Server;
using Core.Server.Command;
using server.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketService : MonoBehaviour {

    private PubgSocket pubgSocket;

    public static SocketService instance;
   private   ISyncManager sm;

    private   Action<Action> processCallback = (a) =>
    {
        a();
       
    };

    private CommandsUtils commandsUtils;

    private void Init()
    {
        commandsUtils = new CommandsUtils();
        List<ICommand> loaderList = CommandLoader.Load();
        commandsUtils.RegCommands(loaderList);
    }


    private PostEngine postEngine;

    void Start () {
        instance = this;
        sm = new SyncManager(processCallback);
        pubgSocket = new PubgSocket();
        pubgSocket.callBack = ReceiveData;
        pubgSocket.Init();
        pubgSocket.InitTimer(sm);
        postEngine = new PostEngine();
    }

    public void ReceiveData(string key, string body, string[] paraters)
    {
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
    public void PostData(string method, string[] parameter,System.Action<string> callBack)
    {
        postEngine.PostData(method, parameter, callBack);
    }
    private void Update()
    {
        if(sm!=null)
        {

        }
        sm.Exec();
    }
}
