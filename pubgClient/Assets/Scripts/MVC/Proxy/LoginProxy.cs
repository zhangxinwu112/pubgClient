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
        SocketService.instance.PostData("server.DAO.LoginDao" + Constant.METHOD_SPLIT+ "CheckLogin", new string[] { username, password }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            if(dataResult.result==0)
            {
                string json = Utils.CollectionsConvert.ToJSON(dataResult.data);
                UserItem _user = Utils.CollectionsConvert.ToObject<UserItem>(json);
                if (_user != null)
                {
                    PlayerPrefs.SetString("telephone", _user.telephone);
                    LoginInfo.Userinfo = _user;
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
