using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountFade : Facade, IFacade
{

    private static AccountFade instance = null;

    public static AccountFade GetInstance()
    {
        if (instance == null)
            instance = new AccountFade();
        return instance;
    }

    protected override void InitializeController()
    {
        base.InitializeController();
        RegisterCommand(RegisterNotifications.REGISTER,typeof(RegisterCommand));
    }

   
    protected override void InitializeModel()
    {
        base.InitializeModel();

      //  NGUIDebug.Log("注册");
        RegisterProxy(new RegisterProxy());

    }

    private UserMeditor userMeditor;
    public void StartUp(GameObject root)
    {
        if(userMeditor==null)
        {
            userMeditor = new UserMeditor();
            RegisterMediator(userMeditor);
        }
        userMeditor.Init(root);
    }

   
}
