using server.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketInit : MonoBehaviour {

    private PubgSocket pubgSocket;
    // Use this for initialization
   private   ISyncManager sm;

    private   Action<Action> processCallback = (a) =>
    {
        a();
       
    };



    void Start () {
        sm = new SyncManager(processCallback);
        pubgSocket = new PubgSocket();
        pubgSocket.callBack = ReceiveData;
        pubgSocket.Init();
        pubgSocket.InitTimer(sm);
    }

    public void ReceiveData(string key, string body, string[] paraters)
    {
        NGUIDebug.Log(body);

    }
	
	
    private void OnDestroy()
    {
        if (pubgSocket != null)
        {
            pubgSocket.Close();
        }
    }
    

    private void Update()
    {
        if(sm!=null)
        {

        }
        sm.Exec();
    }
}
