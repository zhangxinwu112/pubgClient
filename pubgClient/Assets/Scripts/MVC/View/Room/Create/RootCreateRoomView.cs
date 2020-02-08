using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RootCreateRoomView : RootBaseRoomView
{
    [SerializeField]
    private Button deleteButton;

    [SerializeField]
    public RoomCreateView roomCreateView;


    [SerializeField]
    public RoomEditView roomEditView;


    [SerializeField]
    private Button enterGameButton;


    [SerializeField]
    private Button setFenceButton;


    void Start () {
        CreateRoomFade.GetInstance().StartUp(gameObject);
    }


    public void ClickDeleteHandleEvent(UnityAction action)
    {
        deleteButton.onClick.AddListener(action);
    }

    /// <summary>
    /// 进入游戏
    /// </summary>
    /// <param name="action"></param>
    public void ClickEnterHandleEvent(UnityAction action)
    {
        enterGameButton.onClick.AddListener(action);
    }

    /// <summary>
    /// 设定电子围栏
    /// </summary>
    /// <param name="action"></param>
    public void ClickFenceHandleEvent(UnityAction action)
    {
        setFenceButton.onClick.AddListener(action);
    }

    public void SetButtonState(bool state)
    {
        enterGameButton.interactable = state;
        setFenceButton.interactable = state;
    }
}
