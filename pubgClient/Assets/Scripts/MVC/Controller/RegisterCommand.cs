using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class RegisterCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        Dictionary<string,string> dic = notification.Body as Dictionary<string, string>;

        string telephone = dic["telephone"].ToString();
        string password = dic["password"].ToString();
        string nick = dic["nickName"].ToString();
        string icon = dic["icon"].ToString();

        RegisterProxy levelProxy = (RegisterProxy)LoginFade.GetInstance().RetrieveProxy(RegisterProxy.NAME);

        levelProxy.Register(telephone, password, nick, icon);
    }
}
