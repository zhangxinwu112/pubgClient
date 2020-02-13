using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class EditGrounpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        Dictionary<string, object> dic = notification.Body as Dictionary<string, object>;
        string grounpName = dic["grounpName"].ToString();
        string checkCode = dic["checkCode"].ToString();
        string grounpId = dic["grounpId"].ToString();
        string playerTime = dic["playerTime"].ToString();
        GrounpEditProxy editGrounpProxy = (GrounpEditProxy)EditGameFade.GetInstance().RetrieveProxy(GrounpEditProxy.NAME);
        editGrounpProxy.EditGrounp( grounpId, grounpName, checkCode, playerTime);



    }
}
