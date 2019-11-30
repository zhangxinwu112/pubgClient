using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RootJoinRoomView : RootBaseRoomView
{

    [SerializeField]
    public Button joinButton;

    [SerializeField]
    public Button exitButton;

  
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


}
