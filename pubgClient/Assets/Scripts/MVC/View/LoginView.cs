using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class LoginView : MonoBehaviour
{

    public InputField userName;

    public InputField passwrod;

    public Text LoginerrorMessage;

    public Button LoginButton;

    private void Start()
    {
        LoginFade.GetInstance().StartUp(gameObject);
        LoginerrorMessage.text = "";
    }

    public void ShowLoginError(string error)
    {
        userName.text = "";
        passwrod.text = "";
        LoginerrorMessage.text = error;

        DOVirtual.DelayedCall(3.0f, () => {
            LoginerrorMessage.text = "";
        });
        LoginButton.interactable = false;
    }

  
    //login
    public void OnClickLoginButton(UnityAction<Button> action)
    {
       // Debug.Log("点击登陆页面!");
        LoginButton.onClick.AddListener(()=> {
            action.Invoke(LoginButton);

        });
       
    }


    public string GetLoginUserName()
    {
        return userName.text.Trim ();
    }

    public string GetLoginPassWord()
    {
        return passwrod.text.Trim();
    }


    public void ShowUserName(string _userName)
    {
        userName.text = _userName;
    }

    //public void EnableLoginButton(bool isEnable)
    //{
    //    if(LoginButton!=null)
    //    {
    //        LoginButton.GetComponent<Button>().interactable = isEnable;
    //    }
    //}

  

}
