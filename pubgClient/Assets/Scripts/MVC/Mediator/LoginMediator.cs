using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.UI;


public class LoginMediator : Mediator
{
    public new const string NAME = "LoginMediator";

    private GameObject root = null;

    public LoginMediator(GameObject _root) : base(NAME)
    {
        this.root = _root;

        //注册按钮的方法
        root.GetComponent<LoginView>().OnClickLoginButton(StartLogin);
        SetCookieUserName();
    }


    private string userName = "";
    private Button button;
    private void StartLogin(Button button)
    {
        this.button = button;
        userName = root.GetComponent<LoginView>().GetLoginUserName();
        string passWord = root.GetComponent<LoginView>().GetLoginPassWord();
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
        {
            root.GetComponent<LoginView>().ShowLoginError("账号和密码不能为空。");
            return;
        }
      //  root.GetComponent<LoginView>().EnableLoginButton(false);
       if(button!=null)
        {
            button.interactable = false;
        }
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("username", userName);
        dic.Add("password", passWord);
        SendNotification(LoginNotifications.LOGIN, dic);
    }

  
    //添加对这些消息的监听
    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        list.Add(LoginNotifications.QUERY_LOGIN_SUCCESS);
        list.Add(LoginNotifications.QUERY_LOGIN_ERROR);
        list.Add(LoginNotifications.SEARCH_USER_NAME_SUCCESS);
        return list;
    }

    //当监听到这些消息的时候  从而触发的一些事件
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
           
            //登录成成功
            case LoginNotifications.QUERY_LOGIN_SUCCESS:

                SceneTools.instance.LoadScene("MachineCode");
                break;

            //登录失败
            case LoginNotifications.QUERY_LOGIN_ERROR:

                string errorMessage = notification.Body as string;
                root.GetComponent<LoginView>().ShowLoginError(errorMessage);
               

                break;

        
            
            default:
                break;
        }
    }

    private void SetCookieUserName()
    {

        string userName = PlayerPrefs.GetString("telephone");
        if(!string.IsNullOrEmpty(userName))
        {
            root.GetComponent<LoginView>().ShowUserName(userName);
        }

    }

}
