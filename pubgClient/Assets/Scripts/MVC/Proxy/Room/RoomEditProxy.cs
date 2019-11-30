using DataModel;
using Model;
using PureMVC.Patterns;
using server.Model;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class RoomEditProxy : Proxy
{
    public new const string NAME = "RoomEditProxy";

    public RoomEditProxy() : base(NAME)
    {

    }

    public void AddRoom(string roomName)
    {
        if(SocketService.instance==null)
        {
            Debug.LogError("SocketService is null");
            return;
        }
        SocketService.instance.PostData("server.DAO.EditRoomDao" + Constant.METHOD_SPLIT+ "AddRoom", new string[] { roomName,LoginInfo.Userinfo.id.ToString(),"shanxi" }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            
            if(dataResult!=null)
            {
                SendNotification(RoomNotifications.CREATE_ROOM_RESULT, dataResult);
            }
     
        });
    }


    public void EditRoom(string roomName,string grounpName,string password,string roomId,string grounpId)
    {
        if (SocketService.instance == null)
        {
            Debug.LogError("SocketService is null");
            return;
        }
        SocketService.instance.PostData("server.DAO.EditRoomDao" + Constant.METHOD_SPLIT + "UpdateRoom", new string[] { roomName, grounpName , password , roomId , grounpId }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            SendNotification(RoomNotifications.EDIT_ROOM_RESULT, dataResult);

        });
    }

    public void DeleteRoom(string  roomid)
    {
        SocketService.instance.PostData("server.DAO.EditRoomDao" + Constant.METHOD_SPLIT + "DeleteRoom", new string[] { roomid }, (result) =>
        {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            SendNotification(RoomNotifications.DELETE_ROOM_RESULT, dataResult);

        });
    }



}
