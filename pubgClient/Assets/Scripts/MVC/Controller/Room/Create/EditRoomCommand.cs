using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class EditRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        Dictionary<string, object> dic = notification.Body as Dictionary<string, object>;
        string roomName = dic["roomName"].ToString();
        string grounpName = dic["grounpName"].ToString();
        string checkCode = dic["checkCode"].ToString();
        string roomId = dic["roomId"].ToString();
        string grounpId = dic["grounpId"].ToString();
        RoomEditProxy editroomProxy = (RoomEditProxy)CreateRoomFade.GetInstance().RetrieveProxy(RoomEditProxy.NAME);
        editroomProxy.EditRoom(roomName, grounpName, checkCode, roomId, grounpId);



    }
}
