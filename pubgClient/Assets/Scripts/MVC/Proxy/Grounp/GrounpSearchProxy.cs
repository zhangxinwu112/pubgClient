using DataModel;
using Model;
using PureMVC.Patterns;
using server.Model;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class GrounpSearchProxy : Proxy
{
    public new const string NAME = "GrounpSearchProxy";

    public GrounpSearchProxy() : base(NAME)
    {

    }

    public void SearchAllGrounp(string userId)
    {
        if(SocketService.instance==null)
        {
            Debug.LogError("SocketService is null");
            return;
        }
        SocketService.instance.PostData("server.DAO.SearchGrounpDao" + Constant.METHOD_SPLIT+ "SearchAllGrounp", new string[] { userId }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            
             string json = Utils.CollectionsConvert.ToJSON(dataResult.data);
             List<Grounp> grounps = Utils.CollectionsConvert.ToObject<List<Grounp>>(json);
            
             SendNotification(RoomNotifications.ALL_GROUNP_SUCCESS, grounps);
           
            // Debug.Log("回调成功："+ rooms.Count);
        });
    }

    public void SearchSingleGrounp(string grounpId)
    {
        SocketService.instance.PostData("server.DAO.SearchGrounpDao" + Constant.METHOD_SPLIT + "SearchSingleGrounp", new string[] { grounpId }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);

            string json = Utils.CollectionsConvert.ToJSON(dataResult.data);
            List<Room> Grounps = Utils.CollectionsConvert.ToObject<List<Room>>(json);
           
           SendNotification(RoomNotifications.SINGLE_ROOM_SUCCESS, Grounps);
          
        });
    }

    public void SearchSingleRoom(string roomId)
    {
        SocketService.instance.PostData("server.DAO.SearchGrounpDao" + Constant.METHOD_SPLIT + "SearchSingleRoom", new string[] { roomId }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);

            string json = Utils.CollectionsConvert.ToJSON(dataResult.data);
            List<UserItem> Grounps = Utils.CollectionsConvert.ToObject<List<UserItem>>(json);

            SendNotification(RoomNotifications.SINGLE_GROUNP_SUCCESS, Grounps);

        });
    }



}
