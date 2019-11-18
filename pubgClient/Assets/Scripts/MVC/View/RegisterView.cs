using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RegisterView : MonoBehaviour {


    [SerializeField]
    public InputField telephone;


    [SerializeField]
    public InputField password;


    [SerializeField]
    public InputField repassword;


    [SerializeField]
    public InputField checkcode;


    [SerializeField]
    public InputField nickName;


    [SerializeField]
    public Dropdown icon;

    [SerializeField]
    public Button submit;

    [SerializeField]
    public Text message;
    [SerializeField]
    public GameObject register;
    [SerializeField]
    public GameObject success;


    [SerializeField]
    public Dropdown usertype;

    private void Start()
    {
        RegisterFade.GetInstance().StartUp(gameObject);
        success.gameObject.SetActive(true);
        register.gameObject.SetActive(true);
        register.transform.localScale = Vector3.one;
        success.transform.localScale = Vector3.zero;
        message.text = "";
    }
   
    public void  Register(UnityAction action)
    {
        submit.onClick.AddListener(action);
    }

    public void ShowError(string error)
    {
        message.text = error;

        DOVirtual.DelayedCall(4.0f, () => {
            message.text = "";
        });
    }

    public void SetSuccessView()
    {
        register.transform.localScale = Vector3.zero;
        success.transform.localScale = Vector3.one;
    }
}
