using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RootRoomView : MonoBehaviour {


    [SerializeField]
    public RoomListView roomListView;


    [SerializeField]
    public Button deleteButton;

    [SerializeField]
    public RoomCreateView roomCreateView;


    [SerializeField]
    public RoomEditView roomEditView;


    [SerializeField]
    public ErrorMessage errorMessage;


    void Start () {
        CreateRoomFade.GetInstance().StartUp(gameObject);
    }

    private UnityAction<string> sinlgeRoomAction;
    public void SearchSingleRoomAction(UnityAction<string> sinlgeRoomAction)
    {
        this.sinlgeRoomAction = sinlgeRoomAction;
    }

    public void CallSearchSingleRoomAction(string id)
    {
        if (sinlgeRoomAction != null && !string.IsNullOrEmpty(id))
        {
            sinlgeRoomAction.Invoke(id);
        }
    }

    private UnityAction<string> sinlgGrounpAction;
    public void SearchSingleGrounpAction(UnityAction<string> sinlgeGrounpAction)
    {
        this.sinlgGrounpAction = sinlgeGrounpAction;
    }
    public void CallSearchSingleGrounpAction(string id)
    {

        if (sinlgGrounpAction != null && !string.IsNullOrEmpty(id))
        {
            sinlgGrounpAction.Invoke(id);
        }
    }

}
