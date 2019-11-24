using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class SearchSinlgeRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        RoomProxy roomProxy = (RoomProxy)CreateRoomFade.GetInstance().RetrieveProxy(RoomProxy.NAME);
        roomProxy.SearchSingleRoom(notification.Body.ToString());
    }
}
