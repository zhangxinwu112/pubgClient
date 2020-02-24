using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class JoinRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {

        RoomJoinProxy roomJoinProxy = (RoomJoinProxy)JoinRoomFade.GetInstance().RetrieveProxy(RoomJoinProxy.NAME);

        Dictionary<string, string> dic = notification.Body as Dictionary<string, string>;
        string checkCode = dic["checkCode"].ToString();
        string roomId = dic["roomId"].ToString();
        string gameId = dic["gameId"].ToString();
  
        roomJoinProxy.JoinRoom(checkCode, gameId,roomId);



    }
}
