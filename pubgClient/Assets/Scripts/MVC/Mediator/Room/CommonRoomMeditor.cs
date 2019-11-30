using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonRoomMeditor : Mediator
{

    public CommonRoomMeditor(string NAME) : base(NAME)
    {

    }

    protected void ShowListData()
    {
        SendNotification(RoomNotifications.ALL_ROOM);
    }
}
