using Model;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class MachineCodeProxy : Proxy
{
    public new const string NAME = "MachineCodeProxy";

    public MachineCodeProxy() : base(NAME)
    {

    }

    public void CodeSubmit(string activeCode)
    {
        //string serverPath = Config.parse("ServerPath");

        string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
        string flat = SystemInfo.deviceType.ToString();
        string method = "server.Code" + Constant.METHOD_SPLIT + "CheckCode";

        SocketService.instance.PostData(method, new string[] { activeCode,
            deviceUniqueIdentifier, flat, SystemInfo.operatingSystem}, (result) => {

            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            if (dataResult.result == 0)
            {
                SendNotification(LoginNotifications.QUERY_CODE_LOGIN_SUCCESS);
            }
            else
            {
                SendNotification(LoginNotifications.QUERY_CODE_LOGIN_ERROR, dataResult.resean);
            }
         
        });

    }

   
}
