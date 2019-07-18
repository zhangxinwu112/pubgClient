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

    public void StartUp(GameObject root)
    {
        RegisterMediator(new LoginMediator(root));
    }

   
}
