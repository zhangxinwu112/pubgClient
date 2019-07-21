using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterMeditor : Mediator
{
    public new const string NAME = "RigisterMeditor";

    private GameObject root = null;

    public RegisterMeditor(GameObject _root) : base(NAME)
    {
        this.root = _root;

        root.GetComponent<RegisterView>().Register(StartRegiter); 

    } 


    public void StartRegiter()
    {
        string telephone = root.GetComponent<RegisterView>().telephone.text.Trim();
        string password = root.GetComponent<RegisterView>().password.text.Trim();
        string repasword = root.GetComponent<RegisterView>().repassword.text.Trim();
        string checkCode = root.GetComponent<RegisterView>().checkcode.text.Trim();
        string nickName = root.GetComponent<RegisterView>().nickName.text.Trim();

        Dictionary<string, string> dic = new Dictionary<string, string>();


        dic.Add("telephone", telephone);
        dic.Add("password", password);
        dic.Add("checkCode", checkCode);
        dic.Add("nickName", nickName);


        SendNotification(RegisterNotifications.REGISTER, dic);

    }

    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        list.Add(RegisterNotifications.REGISTER_SUCCESS);
        list.Add(RegisterNotifications.REGISTER_FAILTURE);
        return list;
    }
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {

            //校验成功
            case RegisterNotifications.REGISTER_SUCCESS:

                //SceneTools.instance.LoadScene("Game");
                break;

            //校验失败
            case RegisterNotifications.REGISTER_FAILTURE:

                string errorMessage = notification.Body as string;
               // root.GetComponent<CodeView>().ShowMessage(errorMessage);
                break;
            default:
                break;
        }
    }






 }
