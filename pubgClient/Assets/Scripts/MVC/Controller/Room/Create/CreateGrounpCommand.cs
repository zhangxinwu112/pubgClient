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

        Dictionary<string, string> dic = notification.Body as Dictionary<string, string>;
        string createName = dic["createName"].ToString();
        string playerTime = dic["playerTime"].ToString();

        editGrounpProxy.AddGrounp(createName, playerTime);
    }
}
