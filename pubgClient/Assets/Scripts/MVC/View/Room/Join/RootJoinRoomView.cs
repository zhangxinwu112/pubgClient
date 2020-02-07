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


    void Start () {

        JoinRoomFade.GetInstance().StartUp(gameObject);
        enterButton.interactable = false;
        StartCoroutine(CheckEnterState());
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

            string grounpId = GetComponentInChildren<RoomListView>().selectGrounpId;
            if(!string.IsNullOrEmpty(grounpId))
            {
                action.Invoke(grounpId);
            }
           
        });
    }
    //退出room
    public void ClickExitHandleEvent(UnityAction<string> action)
    {
        exitButton.onClick.AddListener(()=> {
            string grounpId = GetComponentInChildren<RoomListView>().selectGrounpId;
            if(!string.IsNullOrEmpty(grounpId))
            {
                action.Invoke(grounpId);
            }
        });
    }

    private UnityAction callBack;

    public void ButtonStateCallBack(UnityAction callBack)
    {
        this.callBack = callBack;
    }

    private IEnumerator CheckEnterState()
    {
        while(true)
        {
            if (callBack != null)
            {
                callBack.Invoke();
            }

            yield return new WaitForSeconds(2.0f);
           
        }
    }

    public void SetEnterButtonActive(bool isActive)
    {
        enterButton.interactable = isActive;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }


}
