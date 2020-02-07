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

        uniWebView = GetComponent<UniWebView>();

        if (uniWebView == null)
        {
            uniWebView = gameObject.AddComponent<UniWebView>();
            uniWebView.ReferenceRectTransform = transform.parent.GetComponent<RectTransform>();
        }
        uniWebView.OnMessageReceived += FunctionCallBack;
        string mapUrl = Config.parse("Map2dAddress");
        OpenWebPage(mapUrl);
        StartCoroutine(UpdatePostion());
        UIUtility.LockScreen(8.0f);

        //if (uniWebView != null)
        //{
        //    NGUIDebug.Log("callBack");
        //    DOVirtual.DelayedCall(3.0f, () => {

        //        uniWebView.OnMessageReceived += FunctionCallBack;
        //    });


        //}
        uniWebView.AddJavaScript("function Exit( ) { }", completionHandler: _ => {
            //webView.EvaluateJavaScript("add(4, 5);", completionHandler: (payload) => {
            //    print(payload.resultCode); // => "0"
            //    print(payload.data);  // => "9"
            //});
        });



    }

    private UniWebView uniWebView;
    public void OpenWebPage(string mapUrl)
    {
       
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

    private void FunctionCallBack(UniWebView webView, UniWebViewMessage message)
    {
        NGUIDebug.Log(message.Path);
        switch (message.Path)
        {
            //退出
            case "Exit":
                {

                    Application.Quit();
                    break;
                };
         

    
        }

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
    //public void Show(double lon,double lat)
    //{
       
    //    string functionName = "CreateMarker" + "("+ lon+","+ lat+")";
      
    //    GetComponent<UniWebView>().EvaluateJavaScript(functionName);
    //}
    public void Show(string json)
    {
        //MessageShow.instance.ShowMesage(TimeUtils.GetCurrentTimestamp()+"："+ json);

        string functionName = "CreateMapAndMarker" + "(" + json + ")";
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
      //  MessageShow.instance.ShowMesage("isEnabledByUser="+ Input.location.isEnabledByUser+":"+ Application.internetReachability);
        if (Input.location.isEnabledByUser && Application.internetReachability != NetworkReachability.NotReachable)
        {
            
            float lat = Input.location.lastData.latitude;
            float lon = Input.location.lastData.longitude;
            double[] datas = GPSTools.gps84_To_Gcj02(lat, lon);
            double _lat = datas[0];
            double _lon = datas[1];
            GPSItem gpsItem = new GPSItem();
           // gpsItem.telephone = LoginInfo.Userinfo.telephone;
            gpsItem.userName = LoginInfo.Userinfo.name;
            gpsItem.userId = LoginInfo.Userinfo.id;
            gpsItem.lat = _lat;
            gpsItem.lon = _lon;
            gpsItem.userType = LoginInfo.Userinfo.type;
            string json = Utils.CollectionsConvert.ToJSON(gpsItem);
            string sendData = CommandName.UpdatePosition.ToString() + Constant.START_SPLIT + json;
            SocketService.instance.SendData(sendData);
        }
        else
        {
            GPSItem gpsItem = new GPSItem();
            // gpsItem.telephone = LoginInfo.Userinfo.telephone;
            gpsItem.userName = LoginInfo.Userinfo.name;
            gpsItem.userId = LoginInfo.Userinfo.id;
            gpsItem.lat = 34.218229;
            gpsItem.lon = 108.964176;
            gpsItem.userType = LoginInfo.Userinfo.type;
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
        dic.Add("userName", LoginInfo.Userinfo.name);
        list.Add(dic);
        Show(Utils.CollectionsConvert.ToJSON(list));
    }
}
