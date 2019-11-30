using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class SearchGrounpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {

        RoomSearchProxy roomProxy = (RoomSearchProxy)RoomFade.GetInstance().RetrieveProxy(RoomSearchProxy.NAME);
        roomProxy.SearchSingleGrounp(notification.Body.ToString());
    }
}
