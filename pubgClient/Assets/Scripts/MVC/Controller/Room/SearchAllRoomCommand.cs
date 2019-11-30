using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class SearchAllRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        string type = notification.Body.ToString();
        RoomSearchProxy roomProxy = null;
        string userId = LoginInfo.Userinfo.id.ToString();
        if (type.Equals("0"))
        {
            
            roomProxy = (RoomSearchProxy)CreateRoomFade.GetInstance().RetrieveProxy(RoomSearchProxy.NAME);
        }
        else
        {
            roomProxy = (RoomSearchProxy)JoinRoomFade.GetInstance().RetrieveProxy(RoomSearchProxy.NAME);
            userId = "0";
        }
        
        roomProxy.SearchAllRoom(userId);
    }
}
