using DataModel;
using Model;
using PureMVC.Patterns;
using server.Model;
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
            
             SendNotification(RoomNotifications.ALL_ROOM_SUCCESS, rooms);
           
             
            // Debug.Log("回调成功："+ rooms.Count);
        });
    }

    public void SearchSingleRoom(string roomid)
    {
        SocketService.instance.PostData("server.DAO.SearchRoomDao" + Constant.METHOD_SPLIT + "SearchSingleRoom", new string[] { roomid }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);

            string json = Utils.CollectionsConvert.ToJSON(dataResult.data);
            List<Grounp> Grounps = Utils.CollectionsConvert.ToObject<List<Grounp>>(json);
           
           SendNotification(RoomNotifications.SINGLE_ROOM_SUCCESS, Grounps);
          
        });
    }

    public void SearchSingleGrounp(string grounpId)
    {
        SocketService.instance.PostData("server.DAO.SearchRoomDao" + Constant.METHOD_SPLIT + "SearchSingleGrounp", new string[] { grounpId }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);

            string json = Utils.CollectionsConvert.ToJSON(dataResult.data);
            List<UserItem> Grounps = Utils.CollectionsConvert.ToObject<List<UserItem>>(json);

            SendNotification(RoomNotifications.SINGLE_GROUNP_SUCCESS, Grounps);

        });
    }



}
