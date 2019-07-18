using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class SearchCodeCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        //QueryLocalCodeProxy codeProxy = (QueryLocalCodeProxy)LevelFade.GetInstance().RetrieveProxy(QueryLocalCodeProxy.NAME);
        //codeProxy.Query();
    }
}
