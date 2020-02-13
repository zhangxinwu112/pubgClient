using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class SearchGrounpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {

        GrounpSearchProxy grounpProxy = (GrounpSearchProxy)EditGameFade.GetInstance().RetrieveProxy(GrounpSearchProxy.NAME);
        grounpProxy.SearchSingleRoom(notification.Body.ToString());
    }
}
