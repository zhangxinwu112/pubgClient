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


    [SerializeField]
    public Button enterButton;

    [SerializeField]
    public InputField enterInputField;

    [SerializeField]
    public InputField KeyNameSearchField;


    [SerializeField]
    public CURDView curdView;


    void Start () {

        JoinRoomFade.GetInstance().StartUp(gameObject);
     
        KeyNameSearchField.text = "";
        enterInputField.text = "";
    }

    

    public void EnterRoom(UnityAction action)
    {
        enterButton.onClick.AddListener(action);
    }

    public void KeyNameChangeEvent(UnityAction<string> action)
    {
        KeyNameSearchField.onValueChanged.AddListener((a)=> {

            action.Invoke(KeyNameSearchField.text);
        });
    }
    //加入按钮
    public void ClickJoinHandleEvent(UnityAction<string> action)
    {
        joinButton.onClick.AddListener(()=> {

            string roomId = GetComponentInChildren<ListView>().selectRoomId;
            if(!string.IsNullOrEmpty(roomId))
            {
                action.Invoke(roomId);
            }
            else
            {
               errorMessage.ShowMessage("队列表为空，不能加入。");
            }
           
        });
    }
    //退出room
    public void ClickExitHandleEvent(UnityAction<string> action)
    {
        exitButton.onClick.AddListener(()=> {
            string roomId = GetComponentInChildren<ListView>().selectRoomId;
            if(!string.IsNullOrEmpty(roomId))
            {
                action.Invoke(roomId);
            }
        });
    }

    private UnityAction callBack;

  

  

    public void SetEnterButtonActive(bool isActive)
    {
        enterButton.interactable = isActive;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        JoinRoomFade.GetInstance().DestroyEvent();
    }


}
