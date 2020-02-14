using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class EnterButtonStateCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        //RoomJoinProxy roomJoinProxy = (RoomJoinProxy)JoinRoomFade.GetInstance().RetrieveProxy(RoomJoinProxy.NAME);
       // roomJoinProxy.SearchButtonState();
    }
}
