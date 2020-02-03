using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class CreateGrounpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {

        GrounpEditProxy editGrounpProxy = (GrounpEditProxy)CreateRoomFade.GetInstance().RetrieveProxy(GrounpEditProxy.NAME);
        editGrounpProxy.AddGrounp(notification.Body.ToString());
    }
}
