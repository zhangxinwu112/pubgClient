using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class SearchUserNameCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        //QueryLocalUserNameProxy userNameProxy = (QueryLocalUserNameProxy)LevelFade.GetInstance().RetrieveProxy(QueryLocalUserNameProxy.NAME);
        //userNameProxy.Query();
    }
}
