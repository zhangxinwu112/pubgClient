﻿using PureMVC.Interfaces;
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
using Tool;

public class JoinRoomMeditor : Mediator, IEventListener
{
    public new const string NAME = "JoinRoomMeditor";

    private GameObject root = null;

    public JoinRoomMeditor(GameObject _root) : base(NAME)
    {
        
    }

    public JoinRoomMeditor() : base(NAME)
    {

    }

    public void Init(GameObject _root)
    {
        this.root = _root;
        root.GetComponent<RootJoinRoomView>().ClickJoinHandleEvent(JoinRoom);
        root.GetComponent<RootJoinRoomView>().ClickExitHandleEvent(ExitRoom);

        root.GetComponent<RootJoinRoomView>().SearchSingleRoomAction(SearchSingleRoom);
        root.GetComponent<RootJoinRoomView>().SearchSingleGrounpAction(SearchSingleGrounp);
        //查询进入游戏按钮的状态
        root.GetComponent<RootJoinRoomView>().EnterRoom(EnterButtonHandle);
        //增加修改房间
        root.GetComponent<RootJoinRoomView>().curdView.ClickSubmitHandleEvent(AddEditRoom);

        //删除房间
        root.GetComponent<RootJoinRoomView>().curdView.DeleteRoom(DeleteRoom);
        
        EventMgr.Instance.AddListener(this, EventName.KEY_SEARCH);
        EventMgr.Instance.AddListener(this, EventName.UPDATE_PLAYER_STATE);
        EventMgr.Instance.AddListener(this, EventName.SHOW_MESSAGE);
        SendRequestAllGrounp();
    }



    /// <summary>
    /// 加入房间
    /// </summary>
    private void JoinRoom(string gameId,string roomId)
    {
        string checkCode = root.GetComponent<RootJoinRoomView>().enterInputField.text.Trim();
        if (string.IsNullOrEmpty(checkCode))
        {
            root.GetComponent<RootBaseRoomView>().errorMessage.ShowMessage("加入战队的密码不能为空。", SoundType.Error);
            return;
        }

        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("checkCode", checkCode);
        dic.Add("roomId", roomId);
        dic.Add("gameId", gameId);

        SendNotification(RoomNotifications.JOIN_ROOM, dic);
    }

    private void ExitRoom(string roomId)
    {
        SendNotification(RoomNotifications.EXIT_ROOM, roomId);
    }

  
    /// <summary>
    /// 通过房间查询队
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

    //private void SearchEnterButtonState()
    //{
    //    SendNotification(RoomNotifications.SEARCH_BUTTON_STATE);
    //}

    public void AddEditRoom(string gamePassword,string roomId,string roomName,string roomPassword)
    {
        
        Dictionary<string, string> dic = new Dictionary<string, string>();
        string grounpId = root.GetComponentInChildren<ListView>().selectGameId;
        dic.Add("grounpId", grounpId);
        dic.Add("gamePassword", gamePassword);
        dic.Add("roomId", roomId);
        dic.Add("roomName", roomName);
        dic.Add("roomPassword", roomPassword);
        SendNotification(RoomNotifications.CREATE_EDIT_ROOM, dic);
    }

    public void DeleteRoom(string roomId)
    {
        SendNotification(RoomNotifications.DELETE_ROOM, roomId);
    }

    /// <summary>
    /// 进入按钮
    /// </summary>
    private void EnterButtonHandle()
    {
        SceneTools.instance.LoadScene("Game");

    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        //search
        list.Add(RoomNotifications.ALL_GROUNP_SUCCESS);
        list.Add(RoomNotifications.SINGLE_GROUNP_SUCCESS);
        list.Add(RoomNotifications.SINGLE_ROOM_SUCCESS);

        //join and exit
        list.Add(RoomNotifications.JOIN_ROOM_RESULT);
        list.Add(RoomNotifications.EXIT_ROOM_RESULT);
        list.Add(RoomNotifications.SEARCH_BUTTON_STATE_RESULT);

        list.Add(RoomNotifications.CREATE_EDIT_ROOM_RESULT);
        list.Add(RoomNotifications.DELETE_ROOM_RESULT);

        
        return list;
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {

            //返回grounp列表
            case RoomNotifications.ALL_GROUNP_SUCCESS:

                List<Grounp> rooms = notification.Body as List<Grounp>;
                root.GetComponent<RootJoinRoomView>().roomListView.CreateList(rooms,0);

                break;
            //通过gourp查询room
            case RoomNotifications.SINGLE_ROOM_SUCCESS:

                List<Room> grounps = notification.Body as List<Room>;
                root.GetComponent<RootJoinRoomView>().roomListView.CreateList(grounps,1);
                break;
            //通过room查询userList
            case RoomNotifications.SINGLE_GROUNP_SUCCESS:

                List<UserItem> UserItems = notification.Body as List<UserItem>;
                root.GetComponent<RootJoinRoomView>().roomListView.CreateList(UserItems, 2);
                break;

            case RoomNotifications.SEARCH_BUTTON_STATE_RESULT:

                DataResult dataResult = notification.Body as DataResult;
                if(dataResult.result == 0)
                {
                    bool resultstate = (bool)dataResult.data;
                    root.GetComponent<RootJoinRoomView>().SetEnterButtonActive(resultstate);
                }
               
                break;

            case RoomNotifications.JOIN_ROOM_RESULT:

                ResultcallBack(notification, "成功加入战队");
                break;
            case RoomNotifications.EXIT_ROOM_RESULT:

                ResultcallBack(notification, "成功退出战队");
                break;
            case RoomNotifications.CREATE_EDIT_ROOM_RESULT:
                JoinExitRoomButton.instance.UpdateButonState();
                ResultcallBack(notification, "操作成功");
                break;
            case RoomNotifications.DELETE_ROOM_RESULT:
                JoinExitRoomButton.instance.UpdateButonState();
                ResultcallBack(notification, "操作成功");
                break;


            default:
                break;
        }
    }

    private void  ResultcallBack(INotification notification,string successMessage)
    {
        DataResult dataResult = notification.Body as DataResult;
      
        if (dataResult.result == 0)
        {
            root.GetComponent<RootBaseRoomView>().errorMessage.ShowMessage(successMessage, SoundType.Success);
            SendRequestAllGrounp();

        }
        else
        {
            root.GetComponent<RootBaseRoomView>().errorMessage.ShowMessage(dataResult.resean, SoundType.Error);
        }
    }

    private void SendRequestAllGrounp()
    {
        SendNotification(RoomNotifications.ALL_GROUNP, root.GetComponent<RootJoinRoomView>().KeyNameSearchField.text.Trim());
    }

    public bool HandleEvent(string eventName, IDictionary<string, object> dictionary)
    {
        if(eventName.Equals(EventName.KEY_SEARCH) || eventName.Equals(EventName.UPDATE_PLAYER_STATE))
        {
            SendRequestAllGrounp();
        }
        else if (eventName.Equals(EventName.SHOW_MESSAGE))
        {
            string message = dictionary["message"].ToString();
            root.GetComponent<RootBaseRoomView>().errorMessage.ShowMessage(message, SoundType.Tips);
        }

        return true;
    }

    public void RemoveEvent()
    {
        EventMgr.Instance.RemoveListener(this, EventName.KEY_SEARCH);
        EventMgr.Instance.RemoveListener(this, EventName.UPDATE_PLAYER_STATE);
        EventMgr.Instance.RemoveListener(this, EventName.SHOW_MESSAGE);
    }
}
