using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeFade : Facade, IFacade
{

    private static CodeFade instance = null;

    public static CodeFade GetInstance()
    {
        if (instance == null)
            instance = new CodeFade();
        return instance;
    }

    protected override void InitializeController()
    {
        base.InitializeController();
        RegisterCommand(LoginNotifications.CODE_LOGIN, typeof(CodeCommand));
    }

   
    protected override void InitializeModel()
    {
        base.InitializeModel();

        RegisterProxy(new MachineCodeProxy());

    }

    private CodeMeditor codeMeditor;
    public void StartUp(GameObject root)
    {
        if(codeMeditor==null)
        {
            codeMeditor = new CodeMeditor();
            RegisterMediator(codeMeditor);
        }
        codeMeditor.Init(root);


    }

   
}
