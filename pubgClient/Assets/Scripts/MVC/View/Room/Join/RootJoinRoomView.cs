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


    void Start () {

        JoinRoomFade.GetInstance().StartUp(gameObject);
        enterButton.interactable = false;
        enterButton.onClick.AddListener(()=> {
            SceneTools.instance.LoadScene("Game");

        });

        StartCoroutine(CheckEnterState());
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
