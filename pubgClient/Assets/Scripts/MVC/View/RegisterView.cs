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
        InitTypeData();
    }
   
    public void  Register(UnityAction<Button> action)
    {
        submit.onClick.AddListener(()=> {
            action.Invoke(submit);
        });
    }

    public void ShowError(string error)
    {
        message.text = error;

        DOVirtual.DelayedCall(4.0f, () => {
            message.text = "";
        });
        submit.interactable = true;
    }

    public void SetSuccessView()
    {
        register.transform.localScale = Vector3.zero;
        success.transform.localScale = Vector3.one;
    }

    private List<DropDownItem> DropDownItems = new List<DropDownItem>();
    private void InitTypeData()
    {
        usertype.options.Clear();
        DropDownDataItem optiondata = null;
        DropDownItem playerItem = new DropDownItem(0,"玩家");
        DropDownItem adminItem = new DropDownItem(1, "管理员");
        DropDownItem propItem = new DropDownItem(2, "道具");
        DropDownItems.Add(playerItem);
        DropDownItems.Add(adminItem);
        DropDownItems.Add(propItem);

        foreach (DropDownItem item in DropDownItems)
        {
            optiondata = new DropDownDataItem();
            optiondata.id = item.id;
            optiondata.text = item.name;
            usertype.options.Add(optiondata);
        }
        usertype.value = -1;
    }

    public int GetSelectUserTypeIndex() { 
   
        return usertype.value;
    }

}
