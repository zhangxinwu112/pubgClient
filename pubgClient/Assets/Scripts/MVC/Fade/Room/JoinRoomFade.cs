using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomFade : CommonRoomFade
{

    private static JoinRoomFade instance = null;

    public static JoinRoomFade GetInstance()
    {
        if (instance == null)
            instance = new JoinRoomFade();
        return instance;
    }

    protected override void InitializeController()
    {
        base.InitializeController();

        InitializeCommand();
    }

    protected override void InitializeCommand()
    {
        base.InitializeCommand();
        RegisterCommand(RoomNotifications.JOIN_ROOM, typeof(JoinRoomCommand));
        RegisterCommand(RoomNotifications.EXIT_ROOM, typeof(ExitRoomCommand));
   
    }

    protected override void InitializeModel()
    {
        base.InitializeModel();
        InitializeProxy();
    }
    protected override void InitializeProxy()
    {
        base.InitializeProxy();
       
        RegisterProxy(new RoomJoinProxy());
    }


    public void StartUp(GameObject root)
    {
        RegisterMediator(new JoinRoomMeditor(root));
    }

   
}
