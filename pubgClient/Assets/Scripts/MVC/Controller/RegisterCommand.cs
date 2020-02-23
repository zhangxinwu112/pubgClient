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
        string name = dic["nickName"].ToString();
        string icon = dic["icon"].ToString();
        string checkCode = dic["checkCode"].ToString();
        string userType = dic["userType"].ToString();

       // NGUIDebug.Log(LoginFade.GetInstance().RetrieveProxy(RegisterProxy.NAME));
        RegisterProxy registerProxy = (RegisterProxy)LoginFade.GetInstance().RetrieveProxy(RegisterProxy.NAME);

        registerProxy.Register(telephone, password, name, icon, checkCode, userType);
    }
}
