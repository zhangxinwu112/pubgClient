using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class ExitRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        RoomJoinProxy roomJoinProxy = (RoomJoinProxy)JoinRoomFade.GetInstance().RetrieveProxy(RoomJoinProxy.NAME);
        roomJoinProxy.ExitRoom(notification.Body.ToString());



    }
}
