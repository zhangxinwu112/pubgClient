using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class CheckEnterButtonCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {

        RoomJoinProxy roomJoinProxy = (RoomJoinProxy)JoinRoomFade.GetInstance().RetrieveProxy(RoomJoinProxy.NAME);
        roomJoinProxy.CheckEnterButton(notification.Body.ToString());



    }
}
