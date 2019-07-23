using Model;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class LoginProxy : Proxy
{
    public new const string NAME = "LoginProxy";

    public LoginProxy() : base(NAME)
    {

    }

    public void ToLogin(string username,string password)
    {
        SocketService.instance.PostData("server.Login"+ Constant.METHOD_SPLIT+ "CheckLogin", new string[] { username, password }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            if(dataResult.result==0)
            {
                SendNotification(LoginNotifications.QUERY_LOGIN_SUCCESS);
            }
            else
            {
                SendNotification(LoginNotifications.QUERY_LOGIN_ERROR, dataResult.resean);
            }
           // Debug.Log("回调成功："+ result);
        });
    }

}
