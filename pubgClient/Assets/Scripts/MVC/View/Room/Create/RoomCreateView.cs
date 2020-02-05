using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomCreateView : MonoBehaviour {

    [SerializeField]
    private Button createButton;

    [SerializeField]
    private InputField grounpInputField;

    [SerializeField]
    private InputField playerTimeInputField;

    public void ClickHandleEvent(UnityAction<string,string> action)
    {
        createButton.onClick.AddListener(()=> {

            action.Invoke(grounpInputField.text, playerTimeInputField.text);
        });
    }

    public void ClearContent()
    {
        grounpInputField.text = "";
        playerTimeInputField.text = "30";
    }

}
