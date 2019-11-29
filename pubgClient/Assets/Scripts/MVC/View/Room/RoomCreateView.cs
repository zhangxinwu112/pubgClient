using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomCreateView : MonoBehaviour {

    [SerializeField]
    public Button createButton;

    [SerializeField]
    public InputField inputField;

    public void ClickHandleEvent(UnityAction<string> action)
    {
        createButton.onClick.AddListener(()=> {

            action.Invoke(inputField.text);
        });
    }

}
