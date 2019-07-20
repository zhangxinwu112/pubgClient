using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeMeditor : Mediator
{
    public new const string NAME = "CodeMeditor";

    private GameObject root = null;

    public CodeMeditor(GameObject _root) : base(NAME)
    {
        this.root = _root;

        root.GetComponent<CodeView>().OnClickCheckButton(Check);

    } 

    private void Check()
    {
        string code = root.GetComponent<CodeView>().GetCodeContont();
        if(!string.IsNullOrEmpty(code))
        {
            
            SendNotification(LoginNotifications.CODE_LOGIN, code);

            return;
        }else
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

                SceneTools.instance.LoadScene("Game");
                break;

            //校验失败
            case LoginNotifications.QUERY_CODE_LOGIN_ERROR:

                string errorMessage = notification.Body as string;
                root.GetComponent<CodeView>().ShowMessage(errorMessage);
                break;
            default:
                break;
        }
    }






 }
