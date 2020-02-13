using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameEditView : MonoBehaviour {

    [SerializeField]
    private Button editButton;

  
    [SerializeField]
    private InputField grounpInputField;


    [SerializeField]
    private InputField passwordInputField;

    [SerializeField]
    private InputField playerTimeInputField;


    public void EditClickHandleEvent(UnityAction<string,string,string> action)
    {
        editButton.onClick.AddListener(() => {

           action.Invoke(grounpInputField.text,passwordInputField.text,playerTimeInputField.text);
        });
    }

    public void ShowGameData(int gameId)
    {
        int index = ListData.GetGrounpIndex(gameId);
        if(index==0|| index==-1)
        {
            DisableEdit(true);
        }
        else
        {
            DisableEdit(false);
        }
    }
    public void ShowEditData(string gameName, string playerTime, string password)
    {
        
        grounpInputField.text = gameName;
        playerTimeInputField.text = playerTime;
        passwordInputField.text = password;
        editButton.interactable = true;
    }




    public void DisableEdit(bool flag)
    {
        grounpInputField.interactable = flag;
        playerTimeInputField.interactable = flag;
        passwordInputField.interactable = flag;
        editButton.interactable = flag;
    }


}
