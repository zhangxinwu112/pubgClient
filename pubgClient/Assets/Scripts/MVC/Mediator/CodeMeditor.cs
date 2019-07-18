using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeMeditor : Mediator
{
    public new const string NAME = "CodeMeditor";

    private GameObject root = null;

    public CodeMeditor(GameObject _root) : base(NAME)
    {
        this.root = _root;

        root.GetComponent<CodeView>().OnClickCheckButton(Check);

    } 

    private void Check()
    {
        string code = root.GetComponent<CodeView>().GetCodeContont();
        if(!string.IsNullOrEmpty(code))
        {
            
            SendNotification(LoginNotifications.CODE_LOGIN, code);

            return;
        }else
        {
            root.GetComponent<CodeView>().ShowMessage("激活码不能为空。");
        }
    }
	
	
    
}
