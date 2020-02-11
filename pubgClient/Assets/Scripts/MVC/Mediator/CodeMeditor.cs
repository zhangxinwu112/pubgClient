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
        

    }
    public CodeMeditor() : base(NAME)
    {


    }

    public void Init(GameObject _root)
    {
        this.root = _root;

        root.GetComponent<CodeView>().OnClickCheckButton(Check);

        if (LoginInfo.isCheckActiveCode)
        {
            JumpScene();
        }
        else
        {
            AutoCheck();
        }
      
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
            root.GetComponent<CodeView>().ShowMessage("授权码不能为空。");
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
                LoginInfo.isCheckActiveCode = true;
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
        SceneTools.instance.BackScene();
    }
   


}
