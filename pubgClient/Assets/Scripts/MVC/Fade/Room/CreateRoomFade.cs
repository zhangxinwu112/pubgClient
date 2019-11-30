using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomFade : CommonRoomFade
{

    private static CreateRoomFade instance = null;

    public static CreateRoomFade GetInstance()
    {
        if (instance == null)
            instance = new CreateRoomFade();
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
        RegisterCommand(RoomNotifications.CREATE_ROOM, typeof(CreateRoomCommand));
        RegisterCommand(RoomNotifications.EDIT_ROOM, typeof(EditRoomCommand));
        RegisterCommand(RoomNotifications.DELETE_ROOM, typeof(DeleteRoomCommand));
    }

    protected override void InitializeModel()
    {
        base.InitializeModel();
        InitializeProxy();
    }
    protected override void InitializeProxy()
    {
        base.InitializeProxy();
        RegisterProxy(new RoomEditProxy());
    }


    public void StartUp(GameObject root)
    {
        RegisterMediator(new CreateRoomMeditor(root));
    }

   
}
