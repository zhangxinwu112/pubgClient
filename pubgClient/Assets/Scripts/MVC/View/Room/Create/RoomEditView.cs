using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomEditView : MonoBehaviour {

    [SerializeField]
    public Button editButton;

    [SerializeField]
    public InputField  roomInputField;


    [SerializeField]
    public InputField grounpInputField;


    [SerializeField]
    public InputField passwordInputField;

    public void EditClickHandleEvent(UnityAction<string,string,string> action)
    {
        editButton.onClick.AddListener(() => {

            action.Invoke(roomInputField.text, grounpInputField.text, passwordInputField.text);
        });
    }

    public void ShowRoomName(string roomName)
    {
        roomInputField.text = roomName;
    }

    public void ShowGrounpName(string grounpName)
    {
        grounpInputField.text = grounpName;
    }

    public void ShowCheckCode(string checkCode)
    {
        passwordInputField.text = checkCode;
    }

    public void ClearAll()
    {
        ShowRoomName("");
        ShowGrounpName("");
        ShowCheckCode("");
    }


}
