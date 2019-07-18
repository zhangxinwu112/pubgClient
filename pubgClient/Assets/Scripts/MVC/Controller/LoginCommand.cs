using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class LoginCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        Dictionary<string,string> dic = notification.Body as Dictionary<string, string>;

        string userName = dic["username"].ToString();
        string password = dic["password"].ToString();
        LoginProxy levelProxy = (LoginProxy)LoginFade.GetInstance().RetrieveProxy(LoginProxy.NAME);

        levelProxy.ToLogin(userName, password);
    }
}
