using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

/// <summary>
/// 暂时废弃
/// </summary>
public class DeleteGrounpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        //CURDRoomProxy curdProxy = (CURDRoomProxy)JoinRoomFade.GetInstance().RetrieveProxy(CURDRoomProxy.NAME);

        //string roomId = notification.Body.ToString();
        //curdProxy.DeleteRoom(roomId);

    }
}
