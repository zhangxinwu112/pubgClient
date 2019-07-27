using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFade : Facade, IFacade
{

    private static GameFade instance = null;

    public static GameFade GetInstance()
    {
        if (instance == null)
            instance = new GameFade();
        return instance;
    }

    protected override void InitializeController()
    {
        base.InitializeController();
      
    }

   
    protected override void InitializeModel()
    {
        base.InitializeModel();
    }

    public void StartUp(GameObject root)
    {
        RegisterMediator(new GameMeditor(root));
    }

   
}
