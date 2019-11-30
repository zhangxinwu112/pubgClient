using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFade : Facade, IFacade
{

    private static RoomFade instance = null;

    public static RoomFade GetInstance()
    {
        if (instance == null)
            instance = new RoomFade();
        return instance;
    }

    protected override void InitializeController()
    {
        base.InitializeController();

        //search
        RegisterCommand(RoomNotifications.ALL_ROOM, typeof(SearchAllRoomCommand));
        RegisterCommand(RoomNotifications.SINGLE_ROOM, typeof(SearchSinlgeRoomCommand));
        RegisterCommand(RoomNotifications.SINGLE_GROUNP, typeof(SearchGrounpCommand));

        //eidt
        RegisterCommand(RoomNotifications.CREATE_ROOM, typeof(CreateRoomCommand));
        RegisterCommand(RoomNotifications.EDIT_ROOM, typeof(EditRoomCommand));
        RegisterCommand(RoomNotifications.DELETE_ROOM, typeof(DeleteRoomCommand));

    }


    protected override void InitializeModel()
    {
        base.InitializeModel();
        RegisterProxy(new RoomSearchProxy());
        RegisterProxy(new RoomEditProxy());
    }

    public void StartUp(GameObject root)
    {
        RegisterMediator(new RoomMeditor(root));
    }

   
}
