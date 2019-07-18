using JetBrains.Annotations;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class AppFacade : Facade, IFacade
{
    private static AppFacade instance=null;

    
    public static AppFacade GetInstance()
    {
        if(instance==null)
            instance=new AppFacade();
        return instance;
    }

    protected override void InitializeController()
    {
        base.InitializeController();
        //RegisterCommand(MyNotifications.START,typeof(StartCommand));
        //RegisterCommand(MyNotifications.CommitLogin,typeof(LoginCommand));
    }

    protected override void InitializeModel()
    {
        base.InitializeModel();
        //RegisterProxy(new UserLoginProxy());
    }

    public void StartUp(GameObject root)
    {
       // RegisterMediator(new StartMediator(root));
    }
}