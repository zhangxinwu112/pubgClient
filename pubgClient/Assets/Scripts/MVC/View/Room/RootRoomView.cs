using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RootRoomView : MonoBehaviour {

    // Use this for initialization


    [SerializeField]
    public RoomListView roomListView;

    [SerializeField]
    public Button createButton;

    void Start () {
        CreateRoomFade.GetInstance().StartUp(gameObject);

    }

    public void ClickHandleEvent(UnityAction  action)
    {
        createButton.onClick.AddListener(action);
    }

    private UnityAction<string> sinlgeRoomAction;
    public void SearchSingleRoomAction(UnityAction<string> sinlgeRoomAction)
    {
        this.sinlgeRoomAction = sinlgeRoomAction;
    }

    public void CallSearchSingleRoomAction(string id)
    {
      
        if(sinlgeRoomAction!=null && !string.IsNullOrEmpty(id))
        {
            sinlgeRoomAction.Invoke(id);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
