using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonRoomFade : Facade, IFacade
{

    protected virtual void InitializeCommand()
    {
        RegisterCommand(RoomNotifications.ALL_ROOM, typeof(SearchAllRoomCommand));
        RegisterCommand(RoomNotifications.SINGLE_ROOM, typeof(SearchSinlgeRoomCommand));
        RegisterCommand(RoomNotifications.SINGLE_GROUNP, typeof(SearchGrounpCommand));
    }
    protected virtual void InitializeProxy()
    {
        RegisterProxy(new RoomSearchProxy());
    }

}
