using DataModel;
using Model;
using PureMVC.Patterns;
using server.Model;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class RoomJoinProxy : Proxy
{
    public new const string NAME = "RoomJoinProxy";

    public RoomJoinProxy() : base(NAME)
    {

    }

    public void JoinRoom(string grounpId)
    {
        if(SocketService.instance==null)
        {
            Debug.LogError("SocketService is null");
            return;
        }
        SocketService.instance.PostData("server.DAO.JoinRoomDao" + Constant.METHOD_SPLIT+ "JoinRoom", new string[] { grounpId, LoginInfo.Userinfo.id.ToString()}, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            
            if(dataResult!=null)
            {
                SendNotification(RoomNotifications.JOIN_ROOM_RESULT, dataResult);
            }
     
        });
    }


    public void ExitRoom(string grounpId)
    {
        if (SocketService.instance == null)
        {
            Debug.LogError("SocketService is null");
            return;
        }
        SocketService.instance.PostData("server.DAO.ExitRoomDao" + Constant.METHOD_SPLIT + "ExitRoom", new string[] { grounpId, LoginInfo.Userinfo.id.ToString() }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            SendNotification(RoomNotifications.EXIT_ROOM_RESULT, dataResult);

        });
    }

    
}
