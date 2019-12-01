using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeMeditor : Mediator
{
    public new const string NAME = "CodeMeditor";

    private GameObject root = null;

    public CodeMeditor(GameObject _root) : base(NAME)
    {
        this.root = _root;

        root.GetComponent<CodeView>().OnClickCheckButton(Check);

        AutoCheck();

    }


    private Button button;
    private void Check(Button button)
    {
        this.button = button;
        string code = root.GetComponent<CodeView>().GetCodeContont();
        if(!string.IsNullOrEmpty(code))
        {

            button.interactable = false;
            SendNotification(LoginNotifications.CODE_LOGIN, code);

            return;
        }
        else
        {
            root.GetComponent<CodeView>().ShowMessage("激活码不能为空。");
        }
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        list.Add(LoginNotifications.QUERY_CODE_LOGIN_SUCCESS);
        list.Add(LoginNotifications.QUERY_CODE_LOGIN_ERROR);
        return list;
    }
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {

            //校验成功
            case LoginNotifications.QUERY_CODE_LOGIN_SUCCESS:

                JumpScene(); 
                break;

            //校验失败
            case LoginNotifications.QUERY_CODE_LOGIN_ERROR:

                string errorMessage = notification.Body as string;
                root.GetComponent<CodeView>().ShowMessage(errorMessage);
                button.interactable = true;
                break;
            default:
                break;
        }
    }

    private void AutoCheck()
    {
        string code = PlayerPrefs.GetString("code");
        if(!string.IsNullOrEmpty(code))
        {
            SendNotification(LoginNotifications.CODE_LOGIN, code);
        }
        
    }

    private void JumpScene()
    {
       int userType =  LoginInfo.Userinfo.type;

        switch(userType)
        {
            //玩家
            case 0 :
                {
                    SceneTools.instance.LoadScene("JoinRoom");
                    break;
                }
            //管理员
            case 1:
                {
                    SceneTools.instance.LoadScene("CreateRoom");
                    break;
                }
        }
    }
   


}
