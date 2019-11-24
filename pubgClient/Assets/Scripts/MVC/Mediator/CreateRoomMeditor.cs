using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DataModel;
using server.Model;

public class CreateRoomMeditor : Mediator
{
    public new const string NAME = "CreateRoomMeditor";

    private GameObject root = null;

    public CreateRoomMeditor(GameObject _root) : base(NAME)
    {
        this.root = _root;

        root.GetComponent<RootRoomView>().ClickHandleEvent(CreateRoom);
        root.GetComponent<RootRoomView>().SearchSingleRoomAction(SearchSingleRoom);
        root.GetComponent<RootRoomView>().SearchSingleGrounpAction(SearchSingleGrounp);
        
        SendNotification(RoomNotifications.ALL_ROOM);
    }

    /// <summary>
    /// 创建房间
    /// </summary>
    private void CreateRoom()
    {

    }

    /// <summary>
    /// 通过房间查询队
    /// </summary>
    /// <param name="roomid"></param>
    private void SearchSingleRoom(string roomId)
    {
        SendNotification(RoomNotifications.SINGLE_ROOM, roomId);
    }


    private void SearchSingleGrounp(string grounpId)
    {
        SendNotification(RoomNotifications.SINGLE_GROUNP, grounpId);
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        list.Add(RoomNotifications.ALL_ROOM_SUCCESS);
        list.Add(RoomNotifications.SINGLE_GROUNP_SUCCESS);
        list.Add(RoomNotifications.SINGLE_ROOM_SUCCESS);
        return list;
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {

            //返回room列表
            case RoomNotifications.ALL_ROOM_SUCCESS:

                List<Room> rooms = notification.Body as List<Room>;
                root.GetComponent<RootRoomView>().roomListView.CreateList(rooms,0);

                break;
            //通过room查询gourp
            case RoomNotifications.SINGLE_ROOM_SUCCESS:

                List<Grounp> grounps = notification.Body as List<Grounp>;
                root.GetComponent<RootRoomView>().roomListView.CreateList(grounps,1);
                break;
            //通过grounp查询userList
            case RoomNotifications.SINGLE_GROUNP_SUCCESS:

                List<UserItem> UserItems = notification.Body as List<UserItem>;
                root.GetComponent<RootRoomView>().roomListView.CreateList(UserItems, 2);
                break;
            default:
                break;
        }
    }



}
