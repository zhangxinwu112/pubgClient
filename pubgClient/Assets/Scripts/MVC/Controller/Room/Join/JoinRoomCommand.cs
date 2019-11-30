using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class JoinRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {

        RoomJoinProxy roomJoinProxy = (RoomJoinProxy)JoinRoomFade.GetInstance().RetrieveProxy(RoomJoinProxy.NAME);
        roomJoinProxy.JoinRoom(notification.Body.ToString());



    }
}
