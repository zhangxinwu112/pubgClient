using Core.Common.Logging;
using Core.Server.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SCommand]
public class ShowPositionCommand : ICommand
{ 

    private static ILog log = LogManagers.GetLogger("ShowPositionCommand");
    public string Name
    {
        get
        {
            return "ShowPosition";
        }
    }

    private string[] colors = new string[] { "#3366FF", "#CC33FF", "#2EB800", "#B8008A" };
    public object ExecuteCommand(string body, string[] parameter)
    {
        List<GPSItem> gpsItems = Utils.CollectionsConvert.ToObject<List<GPSItem>>(body);

        if(gpsItems==null || gpsItems.Count==0)
        {
           // MessageShow.instance.ShowMesage("发送数据为空");
            return null;
        }
        GPSItem first = null;
       
        for(int i=0;i< gpsItems.Count;i++)
        {
            if(gpsItems[i].telephone.Equals(LoginInfo.Userinfo.telephone))
            {
                first = gpsItems[i];
                gpsItems.RemoveAt(i);
                break;
            }
        }
        if(first!=null)
        {
            gpsItems.Insert(0, first);
        }
       
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        foreach(GPSItem item in gpsItems)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("lon", item.lon);
            dic.Add("lat", item.lat);

            dic.Add("color", colors[UnityEngine.Random.Range(0, colors.Length)]);
            
            dic.Add("userName", item.userName);
            //dic.Add("icon", "//a.amap.com/jsapi_demos/static/demo-center/icons/poi-marker-1.png");
            list.Add(dic);
        }
        ShowMapPoint.instacne.Show(Utils.CollectionsConvert.ToJSON(list));
      //  NGUIDebug.Log(body);
        return null;
    }
}
