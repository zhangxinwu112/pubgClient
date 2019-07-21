using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterFade : Facade, IFacade
{

    private static RegisterFade instance = null;

    public static RegisterFade GetInstance()
    {
        if (instance == null)
            instance = new RegisterFade();
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

        
        RegisterProxy(new RegisterProxy());

    }

    public void StartUp(GameObject root)
    {
        RegisterMediator(new RegisterMeditor(root));
    }

   
}
