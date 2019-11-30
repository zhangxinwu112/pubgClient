using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class EditRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        Dictionary<string, object> dic = notification.Body as Dictionary<string, object>;
        string roomName = dic["roomName"].ToString();
        string grounpName = dic["grounpName"].ToString();
        string password = dic["password"].ToString();
        string roomid_grounpid = dic["roomid_grounpid"].ToString();
    }
}
