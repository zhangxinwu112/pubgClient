using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RootJoinRoomView : MonoBehaviour {


    [SerializeField]
    public RoomListView roomListView;


    [SerializeField]
    public Button joinButton;

    [SerializeField]
    public Button exitButton;


    [SerializeField]
    public ErrorMessage errorMessage;

    void Start () {

        JoinRoomFade.GetInstance().StartUp(gameObject);
    }

    public void ClickJoinHandleEvent(UnityAction<string> action)
    {
        joinButton.onClick.AddListener(()=> {

            string grounpid = GetComponentInChildren<RoomListView>().selectGrounpId;
            action.Invoke(grounpid);
        });
    }


    public void ClickExitHandleEvent(UnityAction<string> action)
    {
        exitButton.onClick.AddListener(()=> {
            string grounpid = GetComponentInChildren<RoomListView>().selectGrounpId;
            action.Invoke(grounpid);
        });
    }

    private UnityAction<string> sinlgeRoomAction;
    public void SearchSingleRoomAction(UnityAction<string> sinlgeRoomAction)
    {
        this.sinlgeRoomAction = sinlgeRoomAction;
    }

    public void CallSearchSingleRoomAction(string roomId)
    {
        if (sinlgeRoomAction != null && !string.IsNullOrEmpty(roomId))
        {
            sinlgeRoomAction.Invoke(roomId);
        }
    }

    private UnityAction<string> sinlgGrounpAction;
    public void SearchSingleGrounpAction(UnityAction<string> sinlgeGrounpAction)
    {
        this.sinlgGrounpAction = sinlgeGrounpAction;
    }
    public void CallSearchSingleGrounpAction(string grounId)
    {

        if (sinlgGrounpAction != null && !string.IsNullOrEmpty(grounId))
        {
            sinlgGrounpAction.Invoke(grounId);
        }
    }

}
