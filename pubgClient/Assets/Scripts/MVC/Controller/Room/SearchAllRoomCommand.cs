using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class SearchAllRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        RoomSearchProxy roomProxy = (RoomSearchProxy)CreateRoomFade.GetInstance().RetrieveProxy(RoomSearchProxy.NAME);
        roomProxy.SearchAllRoom();
    }
}
