using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginProxy : Proxy
{
    public new const string NAME = "LoginProxy";

    public LoginProxy() : base(NAME)
    {

    }

    public void ToLogin(string username,string password)
    {
        SocketService.instance.PostData("Login.CheckLogin", new string[] { username, password }, (result) => {


        });
    }

}
