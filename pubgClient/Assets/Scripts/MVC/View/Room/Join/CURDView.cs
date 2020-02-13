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
    private InputField passwordInput;


    [SerializeField]
    private Button SubmitButton;

    [SerializeField]
    private Button closeButton;

    void Start () {


        transform.localScale = Vector3.zero;
        AddButton.onClick.AddListener(AddRoom);
        modifyButton.onClick.AddListener(EditRoom);
        closeButton.onClick.AddListener(CloseRoom);
       
    }
    public void ClickSubmitHandleEvent(UnityAction<string,string,string,string> action)
    {
        SubmitButton.onClick.AddListener(()=> {
            if (string.IsNullOrEmpty(roomName.text.Trim()))
            {
                GetComponentInParent<RootBaseRoomView>().errorMessage.ShowMessage("队名称不能为空。");
                return;
            }

            if (string.IsNullOrEmpty(passwordInput.text.Trim()))
            {
                GetComponentInParent<RootBaseRoomView>().errorMessage.ShowMessage("创建房间的密码不能为空。");
                return;
            }


            CloseRoom();
            action.Invoke(gameId, roomId, roomName.text.Trim(), passwordInput.text.Trim());
        });
    }


    public void DeleteRoom(UnityAction<string> action)
    {
        DeleteButton.onClick.AddListener(()=> {
            action.Invoke("");
        });
    }

    private void AddRoom()
    {
        transform.localScale = Vector3.one;
        roomId = "-1";
        SetGrounpName();
    }

    private void EditRoom()
    {
        transform.localScale = Vector3.one;
        roomId = GetComponentInParent<RootJoinRoomView>().GetComponentInChildren<ListView>().selectRoomId;
        SetGrounpName();
    }

    private void CloseRoom()
    {
        transform.localScale = Vector3.zero;
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



}
