using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RootEditGameView : RootBaseRoomView
{

    [SerializeField]
    public GameEditView gameEditView;


    [SerializeField]
    private Button enterGameButton;


    [SerializeField]
    private Button setFenceButton;


    [SerializeField]
    public InputField keyNames;

    void Start () {
        EditGameFade.GetInstance().StartUp(gameObject);
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
       // enterGameButton.interactable = state;
        setFenceButton.interactable = state;
    }

    private void OnDestroy()
    {
        EditGameFade.GetInstance().DestroyEvent();
    }
}
