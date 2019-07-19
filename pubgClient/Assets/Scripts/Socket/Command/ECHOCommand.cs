using Core.Common.Logging;
using Core.Server.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SCommand]
public class ECHOCommand : ICommand
{ 

    private static ILog log = LogManagers.GetLogger("ECHOCommand");
    public string Name
    {
        get
        {
            return "ECHO";
        }
    }

    public object ExecuteCommand(string body, string[] parameter)
    {

        Debug.Log(body);
        return null;
    }
}
