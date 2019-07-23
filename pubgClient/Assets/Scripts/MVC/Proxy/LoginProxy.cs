using Model;
using PureMVC.Patterns;
using server.Model;
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
                string json = Utils.CollectionsConvert.ToJSON(dataResult.data);
                UserName _user = Utils.CollectionsConvert.ToObject<UserName>(json);
                if (_user != null)
                {
                    PlayerPrefs.SetString("telephone", _user.telephone);
                }
                SendNotification(LoginNotifications.QUERY_LOGIN_SUCCESS, dataResult.data);
            }
            else
            {
                SendNotification(LoginNotifications.QUERY_LOGIN_ERROR, dataResult.resean);
            }
           // Debug.Log("回调成功："+ result);
        });
    }

}
