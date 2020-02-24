using AppClient;
using SuperSocket.ClientEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;

public class TestSocket : MonoBehaviour
{

    // Use this for initialization
    private EasyClient client;
    void Start()
    {
        Init();
    }



    public async void Init()
    {
        client = new EasyClient();
        client.Initialize(new MyTerminatorReceiveFilter(), (request) => {
            // handle the received request
            //syncManager.Add(() =>
            //{
            //    callBack(request.Key, request.Body, request.Parameters);
            //});
            Debug.Log(request.Body);
            //NGUIDebug.Log(request.Body);

        });

        client.Error += (s, e) =>
        {
            NGUIDebug.Log(e.Exception.Message);
        };

        client.Closed += (s, e) => {


        };


        //   client.NewPackageReceived += Client_NewPackageReceived;

       // string IP = Config.parse("ServerIP");
        var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse("192.168.100.68"), 9000));

        if (connected)
        {
          //  NGUIDebug.Log("connet success");
            Debug.Log("connet success");
            // Send data to the server
            client.Send(Encoding.ASCII.GetBytes("ADD*1#2 \r\n"));
        }
    }
}
