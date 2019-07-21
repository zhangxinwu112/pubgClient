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


    public Text message;

    private void Start()
    {
        RegisterFade.GetInstance().StartUp(gameObject);
    }
   
    public void  Register(UnityAction action)
    {
        submit.onClick.AddListener(action);
    }
}
