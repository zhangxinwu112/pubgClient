using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RootBaseRoomView : MonoBehaviour {


    [SerializeField]
    public ListView roomListView;

    [SerializeField]
    public ErrorMessage errorMessage;

   
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
