using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomEditView : MonoBehaviour {

    [SerializeField]
    private Button editButton;

    [SerializeField]
    private InputField  roomInputField;


    [SerializeField]
    private InputField grounpInputField;


    [SerializeField]
    private InputField passwordInputField;

    [SerializeField]
    private InputField playerTimeInputField;


    public void EditClickHandleEvent(UnityAction<string,string,string,string> action)
    {
        editButton.onClick.AddListener(() => {

            action.Invoke(roomInputField.text, grounpInputField.text, passwordInputField.text,playerTimeInputField.text);
        });
    }

    public void ShowRoom(string roomName,string playerTime)
    {
        roomInputField.text = roomName;
        playerTimeInputField.text = playerTime;
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
        ShowRoom("","");
        ShowGrounpName("");
        ShowCheckCode("");
    }


}
