using command;
using server.Model;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;
using DG.Tweening;

public class ShowMapPoint : MonoBehaviour {

    public static ShowMapPoint instacne;
    void Awake()
    {
        instacne = this;
        
    }

    void Start () {
       
        string mapUrl = Config.parse("Map2dAddress");
        OpenWebPage(mapUrl);
        StartCoroutine(UpdatePostion());
        UIUtility.LockScreen(8.0f);

    }

    private UniWebView uniWebView;
    public void OpenWebPage(string mapUrl)
    {
        uniWebView = GetComponent<UniWebView>();
       
        if(uniWebView==null)
        {
            uniWebView = gameObject.AddComponent<UniWebView>();
            uniWebView.ReferenceRectTransform = transform.parent.GetComponent<RectTransform>();
        }
       
        uniWebView.Load(mapUrl);
        
        uniWebView.OnPageFinished += (view, statusCode, url) =>
        {
            view.Show();
            SendFirstData();

        };

        uniWebView.OnShouldClose += (view) => {
            UIUtility.LockScreen();
            DeleteWebPage();
            DOVirtual.DelayedCall(0.1f, () => { OpenWebPage(mapUrl); });
            return true;
        };
    }

    public void DeleteWebPage()
    {
        uniWebView = gameObject.GetComponent<UniWebView>();
        if(uniWebView != null)
        {
            uniWebView.Stop();
            GameObject.Destroy(uniWebView);
            uniWebView = null;
        }
    }

    private float intervalTime = 5.0f;
    public void Show(double lon,double lat)
    {
       
        string functionName = "CreateMarker" + "("+ lon+","+ lat+")";
      
        GetComponent<UniWebView>().EvaluateJavaScript(functionName);
    }
    public void Show(string json)
    {
        string functionName = "CreateMarker" + "(" + json + ")";
        GetComponent<UniWebView>().EvaluateJavaScript(functionName);
    }

     private IEnumerator UpdatePostion()
    {
        while(true)
        {
            SendData();
            yield return new WaitForSeconds(intervalTime);
        }
    }

    private void SendData()
    {
        //  TestData();
        if (Input.location.isEnabledByUser)
        {
            float lat = Input.location.lastData.latitude;
            float lon = Input.location.lastData.longitude;
            double[] datas = GPSTools.gps84_To_Gcj02(lat, lon);
            double _lat = datas[0];
            double _lon = datas[1];
            GPSItem gpsItem = new GPSItem();
            gpsItem.telephone = LoginInfo.Userinfo.telephone;
            gpsItem.userName = LoginInfo.Userinfo.nick;
            gpsItem.lat = _lat;
            gpsItem.lon = _lon;
            string json = Utils.CollectionsConvert.ToJSON(gpsItem);
            string sendData = CommandName.UpdatePosition.ToString() + Constant.START_SPLIT + json;
            SocketService.instance.SendData(sendData);
        }
    }

    private void SendFirstData()
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        Dictionary<string, object> dic = new Dictionary<string, object>();
        float lat = Input.location.lastData.latitude;
        float lon = Input.location.lastData.longitude;
        double[] datas = GPSTools.gps84_To_Gcj02(lat, lon);

      
        dic.Add("lat", datas[0]);
        dic.Add("lon", datas[1]);
        dic.Add("userName", LoginInfo.Userinfo.nick);
        list.Add(dic);
        Show(Utils.CollectionsConvert.ToJSON(list));
    }
}
