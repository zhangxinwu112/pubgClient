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

public class EditGameMeditor : Mediator,IEventListener
{
    public new const string NAME = "EditGameMeditor";

    private GameObject root = null;

    public EditGameMeditor(GameObject _root) : base(NAME)
    {
        //  this.root = _root;


    }

    public EditGameMeditor() : base(NAME)
    {
       


    }

    public void Init(GameObject _root)
    {
        this.root = _root;
        root.GetComponent<RootEditGameView>().gameEditView.EditClickHandleEvent(EditGame);
        root.GetComponent<RootEditGameView>().SearchSingleRoomAction(SearchSingleRoom);
        root.GetComponent<RootEditGameView>().SearchSingleGrounpAction(SearchSingleGrounp);
        root.GetComponent<RootEditGameView>().ClickEnterHandleEvent(() => {
            SceneTools.instance.LoadScene("Game");
        });

        //点击点击围栏
        root.GetComponent<RootEditGameView>().ClickFenceHandleEvent(() => {

            SetFence();
        });
        SendRequestAllGrounp();
        EventMgr.Instance.AddListener(this, Constant.KEY_SEARCH);
    }

    private void SetFence()
    {
        string grounpId = root.GetComponentInChildren<ListView>().selectGameId;
       
        Grounp grounp = ListData.FindGrounpByKey(root.GetComponentInChildren<ListView>().selectGameId);
        GrounpStateProxy.SearchState(grounpId, (result) =>
        {
            result = result.Trim('"');
            if (result.Equals("1"))
            {

                PlayerPrefs.SetString("grounpId", grounpId);
                if (grounp != null)
                {
                    PlayerPrefs.SetFloat("fenceLon", grounp.fenceLon);
                    PlayerPrefs.SetFloat("fenceLat", grounp.fenceLat);
                    PlayerPrefs.SetInt("fenceRadius", grounp.fenceRadius);
                }

                SceneTools.instance.LoadScene("Fence");
            }
            else
            {
                root.GetComponent<RootEditGameView>().errorMessage.ShowMessage(grounp.name + "游戏已启动，无法再次进行编辑器电子围栏");
            }

        });
    }

  
    private void EditGame(string grounpName,string checkCode,string playerTime)
    {
        if (string.IsNullOrEmpty(grounpName))
        {
            root.GetComponent<RootEditGameView>().errorMessage.ShowMessage("游戏名称不能为空。");
            return;
        }

        if (string.IsNullOrEmpty(checkCode))
        {
            root.GetComponent<RootEditGameView>().errorMessage.ShowMessage("游戏密码不能为空。");
            return;
        }

        if (string.IsNullOrEmpty(playerTime))
        {
            root.GetComponent<RootEditGameView>().errorMessage.ShowMessage("游戏时间不能为空。");
            return;
        }

        if (string.IsNullOrEmpty(playerTime))
        {
            root.GetComponent<RootEditGameView>().errorMessage.ShowMessage("游戏时长不能为空。");
            return;
        }
        if (int.Parse(playerTime)<=5)
        {
            root.GetComponent<RootEditGameView>().errorMessage.ShowMessage("游戏时长输入不正确");
            return;
        }


        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("grounpName", grounpName);
        dic.Add("playerTime", playerTime);
        dic.Add("checkCode", checkCode);
        dic.Add("grounpId", root.GetComponent<RootEditGameView>().roomListView.selectGameId);
        SendNotification(RoomNotifications.EDIT_GROUNP, dic);
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
       // list.Add(RoomNotifications.CREATE_GROUNP_RESULT);
        list.Add(RoomNotifications.EDIT_GROUNP_RESULT);
       // list.Add(RoomNotifications.DELETE_GROUNP_RESULT);
        return list;
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {

            //返回grounp列表
            case RoomNotifications.ALL_GROUNP_SUCCESS:

                List<Grounp> grounps = notification.Body as List<Grounp>;
                root.GetComponent<RootEditGameView>().roomListView.CreateList(grounps, 0);

                break;
            //通过grounp查询room
            case RoomNotifications.SINGLE_ROOM_SUCCESS:

                List<Room> rooms = notification.Body as List<Room>;
                root.GetComponent<RootEditGameView>().roomListView.CreateList(rooms, 1);
                break;
            //通过room查询userList
            case RoomNotifications.SINGLE_GROUNP_SUCCESS:

                List<UserItem> UserItems = notification.Body as List<UserItem>;
                root.GetComponent<RootEditGameView>().roomListView.CreateList(UserItems, 2);
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
        if (dataResult.result == 0)
        {
            root.GetComponent<RootEditGameView>().errorMessage.ShowMessage(successMessage);
            //root.GetComponent<RootEditGameView>().gameEditView.ClearAll();
            SendRequestAllGrounp();
        }
        else
        {
            root.GetComponent<RootEditGameView>().errorMessage.ShowMessage(dataResult.resean);
        }
    }

    private void SendRequestAllGrounp()
    {
        SendNotification(RoomNotifications.ALL_GROUNP, root.GetComponent<RootEditGameView>().keyNames.text.Trim());
    }

    public bool HandleEvent(string eventName, IDictionary<string, object> dictionary)
    {
        SendRequestAllGrounp();
        return true;
        //throw new System.NotImplementedException();
    }

    public void RemoveEvent()
    {
        EventMgr.Instance.RemoveListener(this, Constant.KEY_SEARCH);
    }
}
