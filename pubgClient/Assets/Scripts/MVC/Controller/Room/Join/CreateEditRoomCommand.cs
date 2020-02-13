using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class CreateEditRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {

        RoomJoinProxy roomJoinProxy = (RoomJoinProxy)JoinRoomFade.GetInstance().RetrieveProxy(RoomJoinProxy.NAME);

        Dictionary<string, string> dic = notification.Body as Dictionary<string, string>;
        string grounpId = dic["grounpId"].ToString();
        string roomId = dic["roomId"].ToString();
        string roomName = dic["roomName"].ToString();
        string roomPassword = dic["roomPassword"].ToString();
        roomJoinProxy.CreateEditRoom(grounpId, roomId, roomName, roomPassword);



    }
}
