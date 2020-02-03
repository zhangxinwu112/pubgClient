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
        RegisterCommand(RoomNotifications.CREATE_GROUNP, typeof(CreateGrounpCommand));
        RegisterCommand(RoomNotifications.EDIT_GROUNP, typeof(EditGrounpCommand));
        RegisterCommand(RoomNotifications.DELETE_GROUNP, typeof(DeleteGrounpCommand));
    }

    protected override void InitializeModel()
    {
        base.InitializeModel();
        InitializeProxy();
    }
    protected override void InitializeProxy()
    {
        base.InitializeProxy();
        RegisterProxy(new GrounpEditProxy());
    }


    public void StartUp(GameObject root)
    {
        RegisterMediator(new CreateGrounpMeditor(root));
    }

   
}
