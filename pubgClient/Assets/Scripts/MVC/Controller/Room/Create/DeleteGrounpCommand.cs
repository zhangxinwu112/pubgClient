using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class DeleteGrounpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        GrounpEditProxy editGrounpProxy = (GrounpEditProxy)EditGameFade.GetInstance().RetrieveProxy(GrounpEditProxy.NAME);
        editGrounpProxy.DeleteGrounp(notification.Body.ToString());

    }
}
