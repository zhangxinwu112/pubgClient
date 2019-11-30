using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class DeleteRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        RoomEditProxy editroomProxy = (RoomEditProxy)RoomFade.GetInstance().RetrieveProxy(RoomEditProxy.NAME);
        editroomProxy .DeleteRoom(notification.Body.ToString());

    }
}
