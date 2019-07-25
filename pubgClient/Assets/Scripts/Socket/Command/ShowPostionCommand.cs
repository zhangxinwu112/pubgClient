using Core.Common.Logging;
using Core.Server.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SCommand]
public class ShowPostionCommand : ICommand
{ 

    private static ILog log = LogManagers.GetLogger("ShowPostionCommand");
    public string Name
    {
        get
        {
            return "ShowPostion";
        }
    }

    public object ExecuteCommand(string body, string[] parameter)
    {
        ShowMapPoint.instacne.Show(body);
      //  NGUIDebug.Log(body);
        return null;
    }
}
