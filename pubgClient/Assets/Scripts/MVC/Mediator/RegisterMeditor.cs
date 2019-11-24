using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RegisterMeditor : Mediator
{
    public new const string NAME = "RigisterMeditor";

    private GameObject root = null;

    public RegisterMeditor(GameObject _root) : base(NAME)
    {
        this.root = _root;

        root.GetComponent<RegisterView>().Register(StartRegiter); 

    } 


    public void StartRegiter()
    {
        string telephone = root.GetComponent<RegisterView>().telephone.text.Trim();
        if(string.IsNullOrEmpty(telephone))
        {
            root.GetComponent<RegisterView>().telephone.text = "";
            root.GetComponent<RegisterView>().telephone.ActivateInputField();
            root.GetComponent<RegisterView>().ShowError("手机号码不能为空。");
            return;
        }

        if(!CheckTelehpone(telephone))
        {
            root.GetComponent<RegisterView>().telephone.text = "";
            root.GetComponent<RegisterView>().telephone.ActivateInputField();
            root.GetComponent<RegisterView>().ShowError("手机号码格式不正确。");
            return;
        }
        string password = root.GetComponent<RegisterView>().password.text.Trim();

        if(string.IsNullOrEmpty(password))
        {
            root.GetComponent<RegisterView>().password.text = "";
            root.GetComponent<RegisterView>().password.ActivateInputField();
            root.GetComponent<RegisterView>().ShowError("密码不能为空");
            return;
        }
        string repasword = root.GetComponent<RegisterView>().repassword.text.Trim();

        if (string.IsNullOrEmpty(repasword))
        {
            root.GetComponent<RegisterView>().repassword.text = "";
            root.GetComponent<RegisterView>().repassword.ActivateInputField();
            root.GetComponent<RegisterView>().ShowError("再次密码不能为空");
            return;
        }

        if(!repasword.Equals(password))
        {
            root.GetComponent<RegisterView>().ShowError("密码输出不一致");
            return;
        }

        string checkCode = root.GetComponent<RegisterView>().checkcode.text.Trim();
        string nickName = root.GetComponent<RegisterView>().nickName.text.Trim();

        if (string.IsNullOrEmpty(nickName))
        {
            root.GetComponent<RegisterView>().nickName.text = "";
            root.GetComponent<RegisterView>().nickName.ActivateInputField();
            root.GetComponent<RegisterView>().ShowError("昵称输入不能为空");
            return;
        }

        Dictionary<string, string> dic = new Dictionary<string, string>();

        dic.Add("telephone", telephone);
        dic.Add("password", password);
        dic.Add("checkCode", "123456");
        dic.Add("nickName", nickName);
        dic.Add("icon", "image1");


        SendNotification(RegisterNotifications.REGISTER, dic);

    }

    private bool CheckTelehpone(string phone)
    {
        //电信手机号码正则        
        string dianxin = @"^1[3578][01379]\d{8}$";
        Regex dReg = new Regex(dianxin);
        //联通手机号正则        
        string liantong = @"^1[34578][01256]\d{8}$";
        Regex tReg = new Regex(liantong);
        //移动手机号正则        
        string yidong = @"^(134[012345678]\d{7}|1[34578][012356789]\d{8})$";
        Regex yReg = new Regex(yidong);

        if (dReg.IsMatch(phone) || tReg.IsMatch(phone) || yReg.IsMatch(phone))
        {
            return true;
        }
        return false;
    }

    

    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        list.Add(RegisterNotifications.REGISTER_SUCCESS);
        list.Add(RegisterNotifications.REGISTER_FAILTURE);
        return list;
    }
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {

            //注册成功
            case RegisterNotifications.REGISTER_SUCCESS:

                root.GetComponent<RegisterView>().SetSuccessView();
                DOVirtual.DelayedCall(3.0f, () => {
                    SceneTools.instance.LoadScene("Login");
                });
                break;

            //注册失败
            case RegisterNotifications.REGISTER_FAILTURE:

                string errorMessage = notification.Body as string;
                 root.GetComponent<RegisterView>().ShowError(errorMessage);
                break;
            default:
                break;
        }
    }

    private void GetUserTypeIndex(Dropdown Dropdown)
    {
       // string text = Dropdown.
    }

   






 }

public class DropDownDataItem : Dropdown.OptionData
{
    public int id;
}
