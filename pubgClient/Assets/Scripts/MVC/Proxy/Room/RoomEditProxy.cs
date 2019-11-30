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
      
            // Debug.Log("回调成功："+ rooms.Count);
        });
    }


    public void EditRoom(string roomName,string grounpName,string password,string roomid_grounpid)
    {
        if (SocketService.instance == null)
        {
            Debug.LogError("SocketService is null");
            return;
        }
        SocketService.instance.PostData("server.DAO.SearchRoomDao" + Constant.METHOD_SPLIT + "SearchAllRoom", new string[] { LoginInfo.Userinfo.id.ToString() }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);

            string json = Utils.CollectionsConvert.ToJSON(dataResult.data);
            List<Room> rooms = Utils.CollectionsConvert.ToObject<List<Room>>(json);

            SendNotification(RoomNotifications.ALL_ROOM_SUCCESS, rooms);


            // Debug.Log("回调成功："+ rooms.Count);
        });
    }




    public void DeleteRoom(int  roomid)
    {
        //SocketService.instance.PostData("server.DAO.SearchRoomDao" + Constant.METHOD_SPLIT + "SearchSingleGrounp", new string[] { roomid }, (result) => {
        //    DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);

        //    string json = Utils.CollectionsConvert.ToJSON(dataResult.data);
        //    List<UserItem> Grounps = Utils.CollectionsConvert.ToObject<List<UserItem>>(json);

        //    SendNotification(RoomNotifications.SINGLE_GROUNP_SUCCESS, Grounps);

        //});
    }



}
