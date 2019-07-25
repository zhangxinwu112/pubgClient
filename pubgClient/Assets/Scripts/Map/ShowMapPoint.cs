using command;
using server.Model;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class ShowMapPoint : MonoBehaviour {

    public static ShowMapPoint instacne;
    void Start () {
        instacne = this;
        StartCoroutine(GetPostion());
    }

    private float intervalTime = 5.0f;
    public void Show(double lon,double lat)
    {
       
        string functionName = "addMarker" + "("+ lon+","+ lat+")";
        //string functionName = "addMarker1" + "()";
        // NGUIDebug.Log(functionName);
        //GetComponent<UniWebView>().EvaluateJavaScript(functionName);
        GetComponent<UniWebView>().EvaluateJavaScript(functionName);
    }
    public void Show(string json)
    {

        string functionName = "addMarker" + "(" + json + ")";
        GetComponent<UniWebView>().EvaluateJavaScript(functionName);


    }

     private IEnumerator GetPostion()
    {
        while(true)
        {

            SendData();

             yield return new WaitForSeconds(intervalTime);
        }
    }

    private void SendData()
    {
        // TestData();
        if (Input.location.isEnabledByUser)
        {
            float lat = Input.location.lastData.latitude;
            float lon = Input.location.lastData.longitude;
            double[] datas = GPSTools.gps84_To_Gcj02(lat, lon);
            double _lat = datas[0];
            double _lon = datas[1];
            GPSItem gpsItem = new GPSItem();
            gpsItem.userName = LoginInfo.Userinfo.nick;
            gpsItem.lat = _lat;
            gpsItem.lon = _lon;
            string json = Utils.CollectionsConvert.ToJSON(gpsItem);
            string sendData = CommandName.UpdatePosition.ToString() + Constant.START_SPLIT + json;
            SocketService.instance.SendData(sendData);
        }
    }

    //private void TestData()
    //{
    //    GPSItem gpsItem = new GPSItem();
    //    gpsItem.userName = LoginInfo.Userinfo.nick;
    //    gpsItem.lat = UnityEngine.Random.Range(0, 10000);
    //    gpsItem.lon = UnityEngine.Random.Range(0, 10000);
    //    string json = Utils.CollectionsConvert.ToJSON(gpsItem);
    //    string sendData = CommandName.UpdatePosition.ToString() + Constant.START_SPLIT + json;
    //    SocketService.instance.SendData(sendData);
    //}
}
