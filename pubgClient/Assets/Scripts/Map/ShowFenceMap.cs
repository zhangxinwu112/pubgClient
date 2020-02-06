using command;
using server.Model;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;
using DG.Tweening;

public class ShowFenceMap : MonoBehaviour {

    public static ShowFenceMap instacne;
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
        string mapUrl = Config.parse("MapFenceAddress");
        OpenWebPage(mapUrl);
        UIUtility.LockScreen(8.0f);

        //if (uniWebView != null)
        //{
        //    NGUIDebug.Log("callBack");
        //    DOVirtual.DelayedCall(3.0f, () => {

        //        uniWebView.OnMessageReceived += FunctionCallBack;
        //    });


        //}
        //uniWebView.AddJavaScript("function Exit( ) { }", completionHandler: _ => {
        //    //webView.EvaluateJavaScript("add(4, 5);", completionHandler: (payload) => {
        //    //    print(payload.resultCode); // => "0"
        //    //    print(payload.data);  // => "9"
        //    //});
        //});



    }

    private UniWebView uniWebView;
    public void OpenWebPage(string mapUrl)
    {
       
        uniWebView.Load(mapUrl);
        
        uniWebView.OnPageFinished += (view, statusCode, url) =>
        {
            view.Show();
            ShowData();

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
        switch (message.Path)
        {
            //退出
            case "Back":
                {
                    SceneTools.instance.LoadScene("CreateRoom");
                    break;
                };
        
        }

    }

    //private void Update()
    //{
    //    if(Input.GetKeyUp(KeyCode.A))
    //    {
    //        SceneTools.instance.LoadScene("CreateRoom");
    //    }
    //}

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

   
    private void ShowData()
    {

        double _lat = 0.0;
        double _lon = 0.0;
  
        if (PlayerPrefs.GetFloat("fenceLat")<=0)
        {
            float lat = Input.location.lastData.latitude;
            float lon = Input.location.lastData.longitude;
            double[] datas = GPSTools.gps84_To_Gcj02(lat, lon);

            _lat = datas[0];
            _lon = datas[1];
        }
        else
        {
            _lat = PlayerPrefs.GetFloat("fenceLat");
            _lon = PlayerPrefs.GetFloat("fenceLon");
        }
       

        string grounpId = PlayerPrefs.GetString("grounpId");
        int fenceRadius = PlayerPrefs.GetInt("fenceRadius");
      
        if (string.IsNullOrEmpty(grounpId))
        {
            grounpId = "96";
        }
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("grounpId", grounpId);
        dic.Add("fenceRadius", fenceRadius);
        dic.Add("lat", _lat);
        dic.Add("lon", _lon);
        dic.Add("ip", Config.parse("ServerIP"));
        string json = Utils.CollectionsConvert.ToJSON(dic);
        string functionName = "CreateMap" + "(" + json + ")";
        //不能传递多个参数，网页插件有限制
        GetComponent<UniWebView>().EvaluateJavaScript(functionName);
    }

   

   

   
}
