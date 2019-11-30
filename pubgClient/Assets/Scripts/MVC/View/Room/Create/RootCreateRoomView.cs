using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RootCreateRoomView : RootBaseRoomView
{
    [SerializeField]
    public Button deleteButton;

    [SerializeField]
    public RoomCreateView roomCreateView;


    [SerializeField]
    public RoomEditView roomEditView;


    void Start () {
        CreateRoomFade.GetInstance().StartUp(gameObject);
    }


    public void ClickDeleteHandleEvent(UnityAction action)
    {
        deleteButton.onClick.AddListener(action);
    }
}
