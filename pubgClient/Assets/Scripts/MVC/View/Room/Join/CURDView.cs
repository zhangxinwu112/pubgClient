using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CURDView : MonoBehaviour {

    [SerializeField]
    private Button AddButton;


    [SerializeField]
    private Button modifyButton;

    [SerializeField]
    private Button DeleteButton;

    [SerializeField]
    private InputField GameName;


    [SerializeField]
    private InputField roomName;

    [SerializeField]
    private InputField roomCreatePasswordInput;


    [SerializeField]
    private InputField gamePasswordInput;


    [SerializeField]
    private Button SubmitButton;

    [SerializeField]
    private Button closeButton;


    [SerializeField]
    private Button titleCloseButton;

    void Start () {


        transform.localScale = Vector3.zero;
        AddButton.onClick.AddListener(AddRoom);
        modifyButton.onClick.AddListener(EditRoom);
        closeButton.onClick.AddListener(CloseRoom);
        titleCloseButton.onClick.AddListener(CloseRoom);

    }
    public void ClickSubmitHandleEvent(UnityAction<string,string,string,string> action)
    {
        SubmitButton.onClick.AddListener(()=> {
            if (string.IsNullOrEmpty(roomName.text.Trim()))
            {
                GetComponentInParent<RootBaseRoomView>().errorMessage.ShowMessage("队名称不能为空。");
                return;
            }

            if (string.IsNullOrEmpty(gamePasswordInput.text.Trim()))
            {
                GetComponentInParent<RootBaseRoomView>().errorMessage.ShowMessage("游戏密码不能为空。");
                return;
            }


            if (string.IsNullOrEmpty(roomCreatePasswordInput.text.Trim()))
            {
                GetComponentInParent<RootBaseRoomView>().errorMessage.ShowMessage("创建房间的密码不能为空。");
                return;
            }


            CloseRoom();
            action.Invoke(gamePasswordInput.text.Trim(), roomId, roomName.text.Trim(), roomCreatePasswordInput.text.Trim());
        });
    }


    public void DeleteRoom(UnityAction<string> action)
    {
        DeleteButton.onClick.AddListener(()=> {
           string roomId = GetComponentInParent<RootJoinRoomView>().GetComponentInChildren<ListView>().selectRoomId;
            action.Invoke(roomId);
        });
    }

    private void AddRoom()
    {
        transform.localScale = Vector3.one;
        roomId = "-1";
        roomName.text = "";
        gamePasswordInput.text = "";
        roomCreatePasswordInput.text = "";
        ShowHideList(false);
        SetGrounpName();
    }

    private void EditRoom()
    {
        transform.localScale = Vector3.one;
        ShowHideList(false);
        roomId = GetComponentInParent<RootJoinRoomView>().GetComponentInChildren<ListView>().selectRoomId;
        Room room = ListData.FindRoomByKey(roomId);
        if(room!=null)
        {
            roomName.text = room.name.Trim();
            gamePasswordInput.text = "";
            roomCreatePasswordInput.text = room.checkCode.Trim();
        }

        SetGrounpName();
    }

    private void CloseRoom()
    {
        transform.localScale = Vector3.zero;
        ShowHideList(true);
    }

    private string gameId;
    private string roomId;
    private void SetGrounpName()
    {
        gameId =  GetComponentInParent<RootJoinRoomView>().GetComponentInChildren<ListView>().selectGameId;
        Grounp grounp = ListData.FindGrounpByKey(gameId);
        if(grounp!=null)
        {
            GameName.text = grounp.name;
        }
    }

    private void ShowHideList(bool isShow)
    {
        if(isShow)
        {
            GetComponentInParent<RootJoinRoomView>().GetComponentInChildren<ListView>().transform.localScale = Vector3.one;
        }
        else
        {
            GetComponentInParent<RootJoinRoomView>().GetComponentInChildren<ListView>().transform.localScale = Vector3.zero;
        }
       
    }

    public void SetTitle()
    {

    }



}
