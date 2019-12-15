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

public class JoinRoomMeditor : Mediator
{
    public new const string NAME = "JoinRoomMeditor";

    private GameObject root = null;

    public JoinRoomMeditor(GameObject _root) : base(NAME)
    {
        this.root = _root;

        root.GetComponent<RootJoinRoomView>().ClickJoinHandleEvent(JoinRoom);
        root.GetComponent<RootJoinRoomView>().ClickExitHandleEvent(ExitRoom);

        root.GetComponent<RootJoinRoomView>().SearchSingleRoomAction(SearchSingleRoom);
        root.GetComponent<RootJoinRoomView>().SearchSingleGrounpAction(SearchSingleGrounp);
        //查询进入游戏按钮的状态
        root.GetComponent<RootJoinRoomView>().ButtonStateCallBack(SearchEnterButtonState);
        root.GetComponent<RootJoinRoomView>().EnterRoom(EnterButtonHandle);
        SendRequestAllRoom();
    }

    /// <summary>
    /// 加入房间
    /// </summary>
    private void JoinRoom(string grounpId)
    {
        string checkCode = root.GetComponent<RootJoinRoomView>().enterInputField.text.Trim();
        if (string.IsNullOrEmpty(checkCode))
        {
            root.GetComponent<RootBaseRoomView>().errorMessage.ShowMessage("加入房间的密码不能为空。");
            return;
        }

        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("checkCode", checkCode);
        dic.Add("grounpId", grounpId);

        SendNotification(RoomNotifications.JOIN_ROOM, dic);
    }

    private void ExitRoom(string grounpId)
    {
        SendNotification(RoomNotifications.EXIT_ROOM, grounpId);
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

    private void SearchEnterButtonState()
    {
        SendNotification(RoomNotifications.SEARCH_BUTTON_STATE);
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
        list.Add(RoomNotifications.ALL_ROOM_SUCCESS);
        list.Add(RoomNotifications.SINGLE_GROUNP_SUCCESS);
        list.Add(RoomNotifications.SINGLE_ROOM_SUCCESS);

        //join and exit
        list.Add(RoomNotifications.JOIN_ROOM_RESULT);
        list.Add(RoomNotifications.EXIT_ROOM_RESULT);
        list.Add(RoomNotifications.SEARCH_BUTTON_STATE_RESULT);
        return list;
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {

            //返回room列表
            case RoomNotifications.ALL_ROOM_SUCCESS:

                List<Room> rooms = notification.Body as List<Room>;
                root.GetComponent<RootJoinRoomView>().roomListView.CreateList(rooms,0);

                break;
            //通过room查询gourp
            case RoomNotifications.SINGLE_ROOM_SUCCESS:

                List<Grounp> grounps = notification.Body as List<Grounp>;
                root.GetComponent<RootJoinRoomView>().roomListView.CreateList(grounps,1);
                break;
            //通过grounp查询userList
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

                ResultcallBack(notification, "成功加入房间");
                break;
            case RoomNotifications.EXIT_ROOM_RESULT:

                ResultcallBack(notification, "成功退出房间");
                break;
          

            default:
                break;
        }
    }

    private void  ResultcallBack(INotification notification,string successMessage)
    {
        DataResult dataResult = notification.Body as DataResult;
        if(root.GetComponent<RootCreateRoomView>()!=null)
        {
            root.GetComponent<RootCreateRoomView>().roomCreateView.ClearContent();
        }
      
        if (dataResult.result == 0)
        {
            root.GetComponent<RootBaseRoomView>().errorMessage.ShowMessage(successMessage);
            SendRequestAllRoom();

        }
        else
        {
            root.GetComponent<RootBaseRoomView>().errorMessage.ShowMessage(dataResult.resean);
        }
    }

    private void SendRequestAllRoom()
    {
        SendNotification(RoomNotifications.ALL_ROOM, "1");
    }



}
