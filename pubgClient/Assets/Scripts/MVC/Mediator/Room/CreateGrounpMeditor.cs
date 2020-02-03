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
using Model;

public class CreateGrounpMeditor : Mediator
{
    public new const string NAME = "CreateGrounpMeditor";

    private GameObject root = null;

    public CreateGrounpMeditor(GameObject _root) : base(NAME)
    {
        this.root = _root;

        root.GetComponent<RootCreateRoomView>().roomCreateView.ClickHandleEvent(CreateRoom);
        root.GetComponent<RootCreateRoomView>().ClickDeleteHandleEvent(DeleteRoom);
        root.GetComponent<RootCreateRoomView>().roomEditView.EditClickHandleEvent(EditRoom);
        root.GetComponent<RootCreateRoomView>().SearchSingleRoomAction(SearchSingleRoom);
        root.GetComponent<RootCreateRoomView>().SearchSingleGrounpAction(SearchSingleGrounp);
        SendRequestAllRoom();
    }

    /// <summary>
    /// 创建队
    /// </summary>
    private void CreateRoom(string createName)
    {
        if(string.IsNullOrEmpty(createName))
        {
            root.GetComponent<RootCreateRoomView>().errorMessage.ShowMessage("创建名称不能为空。");
            return;
        }

        SendNotification(RoomNotifications.CREATE_GROUNP, createName);
    }

    private void EditRoom(string roomName,string grounpName,string checkCode)
    {
        if (string.IsNullOrEmpty(roomName))
        {
            root.GetComponent<RootCreateRoomView>().errorMessage.ShowMessage("修改的房间名称不能为空。");
            return;
        }

        if (string.IsNullOrEmpty(grounpName))
        {
            root.GetComponent<RootCreateRoomView>().errorMessage.ShowMessage("修改的队名称不能为空。");
            return;
        }

        if (string.IsNullOrEmpty(checkCode))
        {
            root.GetComponent<RootCreateRoomView>().errorMessage.ShowMessage("修改的房间的校验密码不能为空。");
            return;
        }

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("roomName", roomName);
        dic.Add("grounpName", grounpName);
        dic.Add("checkCode", checkCode);
        dic.Add("roomId", root.GetComponent<RootCreateRoomView>().roomListView.selectRoomId);
        dic.Add("grounpId", root.GetComponent<RootCreateRoomView>().roomListView.selectGrounpId);
        SendNotification(RoomNotifications.EDIT_GROUNP, dic);
    }

    public void DeleteRoom()
    {
        string selectRoomId = root.GetComponent<RootCreateRoomView>().roomListView.selectRoomId;
        if(string.IsNullOrEmpty(selectRoomId))
        {
            root.GetComponent<RootCreateRoomView>().errorMessage.ShowMessage("当前选择的队为空，请重试。");
            return;
        }
        //root.GetComponent<RootCreateRoomView>().roomEditView.ClearAll();
        SendNotification(RoomNotifications.DELETE_GROUNP, selectRoomId);
    }
    /// <summary>
    /// 通过房间查询分队
    /// </summary>
    /// <param name="roomid"></param>
    private void SearchSingleRoom(string roomId)
    {
        SendNotification(RoomNotifications.SINGLE_ROOM, roomId);
    }

    //通过分队查询用户
    private void SearchSingleGrounp(string grounpId)
    {
        SendNotification(RoomNotifications.SINGLE_GROUNP, grounpId);
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        //search
        list.Add(RoomNotifications.ALL_GROUNP_SUCCESS);
        list.Add(RoomNotifications.SINGLE_GROUNP_SUCCESS);
        list.Add(RoomNotifications.SINGLE_ROOM_SUCCESS);

        //edit
        list.Add(RoomNotifications.CREATE_GROUNP_RESULT);
        list.Add(RoomNotifications.EDIT_GROUNP_RESULT);
        list.Add(RoomNotifications.DELETE_GROUNP_RESULT);
        return list;
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {

            //返回grounp列表
            case RoomNotifications.ALL_GROUNP_SUCCESS:

                List<Grounp> grounps = notification.Body as List<Grounp>;
                root.GetComponent<RootCreateRoomView>().roomListView.CreateList(grounps, 0);

                break;
            //通过grounp查询room
            case RoomNotifications.SINGLE_ROOM_SUCCESS:

                List<Room> rooms = notification.Body as List<Room>;
                root.GetComponent<RootCreateRoomView>().roomListView.CreateList(rooms, 1);
                break;
            //通过room查询userList
            case RoomNotifications.SINGLE_GROUNP_SUCCESS:

                List<UserItem> UserItems = notification.Body as List<UserItem>;
                root.GetComponent<RootCreateRoomView>().roomListView.CreateList(UserItems, 2);
                break;

            case RoomNotifications.CREATE_GROUNP_RESULT:

                ResultcallBack(notification,"队创建成功");
                break;
            case RoomNotifications.DELETE_GROUNP_RESULT:

                ResultcallBack(notification, "删除成功");
                break;
            case RoomNotifications.EDIT_GROUNP_RESULT:

                ResultcallBack(notification, "修改成功");
                break;

            default:
                break;
        }
    }

    private void  ResultcallBack(INotification notification,string successMessage)
    {
        DataResult dataResult = notification.Body as DataResult;
        root.GetComponent<RootCreateRoomView>().roomCreateView.ClearContent();
        if (dataResult.result == 0)
        {
            root.GetComponent<RootCreateRoomView>().errorMessage.ShowMessage(successMessage);
            root.GetComponent<RootCreateRoomView>().roomEditView.ClearAll();
            SendRequestAllRoom();
        }
        else
        {
            root.GetComponent<RootCreateRoomView>().errorMessage.ShowMessage(dataResult.resean);
        }
    }

    private void SendRequestAllRoom()
    {
        SendNotification(RoomNotifications.ALL_GROUNP, "0");
    }



}
