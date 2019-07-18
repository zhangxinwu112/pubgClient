using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class CodeCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
       string data = notification.Body as string;
   
       MachineCodeProxy codeProxy = (MachineCodeProxy)LoginFade.GetInstance().RetrieveProxy(MachineCodeProxy.NAME);

       codeProxy.CodeSubmit(data);
    }
}
