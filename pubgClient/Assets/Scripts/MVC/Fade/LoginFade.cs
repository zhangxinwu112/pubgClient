using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginFade : Facade, IFacade
{

    private static LoginFade instance = null;

    public static LoginFade GetInstance()
    {
        if (instance == null)
            instance = new LoginFade();
        return instance;
    }

    protected override void InitializeController()
    {
        base.InitializeController();
        RegisterCommand(LoginNotifications.LOGIN,typeof(LoginCommand));
        RegisterCommand(LoginNotifications.SEARCH_USER_NAME, typeof(SearchUserNameCommand));
    }

   
    protected override void InitializeModel()
    {
        base.InitializeModel();

        RegisterProxy(new LoginProxy());

    }

    private LoginMediator loginMeditor;
    public void StartUp(GameObject root)
    {
        if(loginMeditor==null)
        {
            loginMeditor = new LoginMediator();
            RegisterMediator(loginMeditor);
        }
        loginMeditor.Init(root);
        
    }

   
}
