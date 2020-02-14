using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class CreateEditRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {


        CURDRoomProxy curdProxy = (CURDRoomProxy)JoinRoomFade.GetInstance().RetrieveProxy(CURDRoomProxy.NAME);

        Dictionary<string, string> dic = notification.Body as Dictionary<string, string>;
        string grounpId = dic["grounpId"].ToString();
        string roomId = dic["roomId"].ToString();
        string roomName = dic["roomName"].ToString();
        string roomPassword = dic["roomPassword"].ToString();
        string gamePassword = dic["gamePassword"].ToString();


        curdProxy.CreateEditRoom(grounpId, gamePassword,roomId, roomName, roomPassword);



    }
}
