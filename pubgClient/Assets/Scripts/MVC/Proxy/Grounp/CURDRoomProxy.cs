using DataModel;
using Model;
using PureMVC.Patterns;
using server.Model;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class CURDRoomProxy : Proxy
{
    public new const string NAME = "CURDRoomProxy";

    public CURDRoomProxy() : base(NAME)
    {

    }



    public void CreateEditRoom(string grounpId,string gamePassword,string roomId,string roomName,string checkCode)
    {
        if (SocketService.instance == null)
        {
            Debug.LogError("SocketService is null");
            return;
        }
        SocketService.instance.PostData("server.DAO.CURDRoomDao" + Constant.METHOD_SPLIT + "CreateEditRoom", new string[] {
            grounpId, gamePassword,roomId, roomName, checkCode,LoginInfo.Userinfo.id.ToString() }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            SendNotification(RoomNotifications.CREATE_EDIT_ROOM_RESULT, dataResult);

        });
    }


    public void DeleteRoom(string roomId)
    {
        if (SocketService.instance == null)
        {
            Debug.LogError("SocketService is null");
            return;
        }
        SocketService.instance.PostData("server.DAO.CURDRoomDao" + Constant.METHOD_SPLIT + "DeleteRoom", new string[] {
            roomId,LoginInfo.Userinfo.id.ToString() }, (result) => {
                DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
                SendNotification(RoomNotifications.DELETE_ROOM_RESULT, dataResult);

            });
    }



}
