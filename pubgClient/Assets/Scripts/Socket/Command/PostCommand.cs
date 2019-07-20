using Core.Common.Logging;
using Core.Server.Command;
using System.Collections;
using System.Collections.Generic;
using Tool;
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
        string[] strs = body.Split(Constant.END_SPLIT.ToCharArray()[0]);
        string methodName = strs[0];
        string data = strs[1];
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("methodName", methodName);
        dic.Add("data", data);

        EventMgr.Instance.SendEvent(EventName.POST_CALLBACK, dic);
        return null;
    }
}
