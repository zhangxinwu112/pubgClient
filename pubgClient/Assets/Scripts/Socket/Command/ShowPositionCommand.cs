﻿using Core.Common.Logging;
using Core.Server.Command;
using DataModel;
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

    private string[] colors = new string[] { "#CC33FF", "#3366FF" };
    public object ExecuteCommand(string body, string[] parameter)
    {
        Dictionary<string,object> resultDic = Utils.CollectionsConvert.ToObject<Dictionary<string, object>>(body);
        string gpsJson = Utils.CollectionsConvert.ToJSON(resultDic["gpsData"]);
        List<GPSItem> gpsItems = Utils.CollectionsConvert.ToObject<List<GPSItem>>(gpsJson);

        string grounpJson = Utils.CollectionsConvert.ToJSON(resultDic["grounp"]);

        Grounp grounp = Utils.CollectionsConvert.ToObject<Grounp>(grounpJson);

        string roomJson = Utils.CollectionsConvert.ToJSON(resultDic["room"]);
        Room room = Utils.CollectionsConvert.ToObject<Room>(roomJson);


        string lifeJson = Utils.CollectionsConvert.ToJSON(resultDic["life"]);
        Life life = Utils.CollectionsConvert.ToObject<Life>(lifeJson);


        if (grounp==null)
        {
            grounp = new Grounp();
        }
        //if (gpsItems==null || gpsItems.Count==0)
        //{
        //   // MessageShow.instance.ShowMesage("发送数据为空");
        //    return null;
        //}
        GPSItem currentUser = null;
       
        for(int i=0; gpsItems!=null && i < gpsItems.Count;i++)
        {
            if(gpsItems[i].userId.Equals(LoginInfo.Userinfo.id))
            {
                gpsItems[i].color = colors[0];
                currentUser = gpsItems[i];
            }
            else
            {
                gpsItems[i].color = colors[1];
            }
        }
        if(currentUser==null)
        {
            currentUser = new GPSItem();
            float lat = Input.location.lastData.latitude;
            float lon = Input.location.lastData.longitude;
            double[] datas = GPSTools.gps84_To_Gcj02(lat, lon);
            currentUser.lat = datas[0];
            currentUser.lon = datas[1];
            currentUser.userId = LoginInfo.Userinfo.id;
            currentUser.userName = LoginInfo.Userinfo.name;
            currentUser.userType = 1;

        }

        Dictionary<string, object> result = new Dictionary<string, object>();
        result.Add("currentUser", currentUser);
        result.Add("gpsData", gpsItems);
        result.Add("grounp", grounp);
        result.Add("room", room);
        result.Add("life", life);
        result.Add("ip", Config.parse("ServerIP"));

        string sendJson = Utils.CollectionsConvert.ToJSON(result);
         //Debug.Log(sendJson);
        ShowMapPoint.instacne.Show(Utils.CollectionsConvert.ToJSON(sendJson));
        return null;
    }
}
