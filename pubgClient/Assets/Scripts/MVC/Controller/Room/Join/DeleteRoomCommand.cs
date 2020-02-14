using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class DeleteRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        CURDRoomProxy curdProxy = (CURDRoomProxy)JoinRoomFade.GetInstance().RetrieveProxy(CURDRoomProxy.NAME);

        string roomId = notification.Body.ToString();
        curdProxy.DeleteRoom(roomId);



    }
}
