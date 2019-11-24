using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomFade : Facade, IFacade
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

        RegisterCommand(RoomNotifications.ALL_ROOM, typeof(SearchAllRoomCommand));
        RegisterCommand(RoomNotifications.SINGLE_ROOM, typeof(SearchSinlgeRoomCommand));
        RegisterCommand(RoomNotifications.SINGLE_GROUNP, typeof(SearchGrounpCommand));

    }

   
    protected override void InitializeModel()
    {
        base.InitializeModel();
        RegisterProxy(new RoomProxy());
    }

    public void StartUp(GameObject root)
    {
        RegisterMediator(new CreateRoomMeditor(root));
    }

   
}
