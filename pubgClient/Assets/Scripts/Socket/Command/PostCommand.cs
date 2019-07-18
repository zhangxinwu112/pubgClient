using Core.Common.Logging;
using Core.Server.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SCommand]
public class PostCommand : ICommand
{ 

    private static ILog log = LogManagers.GetLogger("PostCommand");
    public string Name
    {
        get
        {
            return "Post";
        }
    }

    public object ExecuteCommand(string body, string[] parameter)
    {

        return null;
    }
}
