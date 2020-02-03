using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class SearchAllGrounpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        string type = notification.Body.ToString();
        GrounpSearchProxy grounpProxy = null;
        string userId = LoginInfo.Userinfo.id.ToString();
        if (type.Equals("0"))
        {

            grounpProxy = (GrounpSearchProxy)CreateRoomFade.GetInstance().RetrieveProxy(GrounpSearchProxy.NAME);
        }
        else
        {
            grounpProxy = (GrounpSearchProxy)JoinRoomFade.GetInstance().RetrieveProxy(GrounpSearchProxy.NAME);
            userId = "0";
        }

        grounpProxy.SearchAllGrounp(userId);
    }
}
