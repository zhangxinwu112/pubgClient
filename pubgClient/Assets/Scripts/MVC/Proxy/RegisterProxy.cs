using Model;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class RegisterProxy : Proxy
{
    public new const string NAME = "MachineCodeProxy";

    public RegisterProxy() : base(NAME)
    {

    }

    public void Register(string telephone,string password,string nick,string icon,string checkCode)
    {
    
        string method = "server.Register" + Constant.METHOD_SPLIT + "RegisterUser";

        SocketService.instance.PostData(method, new string[] { telephone,
            password, nick, icon,checkCode}, (result) => {

            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            if (dataResult.result == 0)
            {
                SendNotification(RegisterNotifications.REGISTER_SUCCESS);
            }
            else
            {
                SendNotification(RegisterNotifications.REGISTER_FAILTURE, dataResult.resean);
            }
         
        });

    }

   
}
