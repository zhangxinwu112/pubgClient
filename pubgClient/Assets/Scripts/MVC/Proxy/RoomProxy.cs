using DataModel;
using Model;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class RoomProxy : Proxy
{
    public new const string NAME = "RoomProxy";

    public RoomProxy() : base(NAME)
    {

    }

    public void SearchAllRoom()
    {
        SocketService.instance.PostData("server.DAO.SearchRoomDao" + Constant.METHOD_SPLIT+ "SearchAllRoom", new string[] { LoginInfo.Userinfo.id.ToString() }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            
             string json = Utils.CollectionsConvert.ToJSON(dataResult.data);
             List<Room> rooms = Utils.CollectionsConvert.ToObject<List<Room>>(json);
            if(rooms!=null && rooms.Count>0)
            {
                SendNotification(RoomNotifications.ALL_ROOM_SUCCESS, rooms);
            }
             
            // Debug.Log("回调成功："+ rooms.Count);
        });
    }

    public void SearchSingleRoom(string roomid)
    {
        SocketService.instance.PostData("server.DAO.SearchRoomDao" + Constant.METHOD_SPLIT + "SearchAllRoom", new string[] { LoginInfo.Userinfo.id.ToString() }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);

            string json = Utils.CollectionsConvert.ToJSON(dataResult.data);
            List<Room> rooms = Utils.CollectionsConvert.ToObject<List<Room>>(json);
            if (rooms != null && rooms.Count > 0)
            {
                SendNotification(RoomNotifications.ALL_ROOM_SUCCESS, rooms);
            }

            // Debug.Log("回调成功："+ rooms.Count);
        });
    }



}
