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
        List<GPSItem> gpsItems = Utils.CollectionsConvert.ToObject<List<GPSItem>>(body);
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        foreach(GPSItem item in gpsItems)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("lon", item.lon);
            dic.Add("lat", item.lat);
            dic.Add("userName", item.userName);
            if(item.telephone.Equals(LoginInfo.Userinfo.telephone))
            {
                dic.Add("current", true);
            }
            else
            {
                dic.Add("current", false);
            }

            list.Add(dic);
        }
        ShowMapPoint.instacne.Show(Utils.CollectionsConvert.ToJSON(list));
      //  NGUIDebug.Log(body);
        return null;
    }
}
