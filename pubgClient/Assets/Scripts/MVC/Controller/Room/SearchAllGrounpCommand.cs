using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class SearchAllGrounpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int Usertype = LoginInfo.Userinfo.type;
        string keyName = notification.Body.ToString();

        GrounpSearchProxy grounpProxy = null;
        if (Usertype == 0)
        {

            grounpProxy = (GrounpSearchProxy)EditGameFade.GetInstance().RetrieveProxy(GrounpSearchProxy.NAME);
        }
        else
        {
            grounpProxy = (GrounpSearchProxy)JoinRoomFade.GetInstance().RetrieveProxy(GrounpSearchProxy.NAME);
          
        }

        if(string.IsNullOrEmpty("keyName"))
        {
            keyName = "-1";
        }
        grounpProxy.SearchAllGrounp(keyName);
    }
}
