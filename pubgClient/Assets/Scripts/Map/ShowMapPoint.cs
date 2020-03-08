using command;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

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
            ShowRemianTime();

        };

        uniWebView.OnShouldClose += (view) => {
            UIUtility.LockScreen();
            DeleteWebPage();
            DOVirtual.DelayedCall(0.1f, () => { OpenWebPage(mapUrl); });
            return true;
        };
      
    }

    private void ShowRemianTime()
    {
        string url = "http://" + Config.parse("ServerIP") + ":8899/GetRemainTime/" + LoginInfo.Userinfo.id + "/ " + LoginInfo.Userinfo.type;

        ResourceUtility.Instance.GetHttpText(url, (result) => {
            CallWebPageFunction("RemianTime", result);
        });
    }

    private void FunctionCallBack(UniWebView webView, UniWebViewMessage message)
    {
        switch (message.Path)
        {
            case "Back":
                {
                    SceneTools.instance.BackScene();
                   
                    break;
                };
            case "GameOver":
                {
                    SceneTools.instance.LoadScene("ScoreOrder");

                    break;
                };
            //管理员获取管理的数据
            case "GetRoomList":
                {

                    ReqestData(message, "GetRoomUserTreeData", (result) => {

                        CallWebPageFunction("ShowRoomList",result);
                    });
                   


                    break;
                }
            //玩家之间共享信息
            case "ShowPlayInfo":
                {
                    ReqestData(message, "GetRoomLifeInfoByUser", (result) => {

                        CallWebPageFunction("GetRoomLifeInfoByUser",result);
                    });


                  
                    break;
                }
            //管理员查看用户详细信息
            case "ShowPlayerDetailInfoList":
                {
                    ReqestData(message, "GetPlayerLifeInfoByUser", (result) => {

                        CallWebPageFunction("ShowPlayerDetailInfo",result);
                    });
                    break;

                }
            //管理员查看用户详细信息
            case "AddPlayerLife":
                {
                    string userId = message.Args["userId"].ToString();
                    string addLifeValue = message.Args["addLifeValue"].ToString();
                    string currentUser = message.Args["currentUser"].ToString();
                    string url = "http://" + Config.parse("ServerIP") + ":8899/AddPlayerLife/"  + userId + "/"+ addLifeValue + "/"+ currentUser;

                    ResourceUtility.Instance.GetHttpText(url, (result) => {
                        CallWebPageFunction("AddPlayerLifeMessageShow", result);
                    });
                    break;

                }
        }
 
    }

    private  void ReqestData(UniWebViewMessage message,string method,System.Action<string > callBack)
    {
        string userId = message.Args["userId"].ToString();
        string url = "http://" + Config.parse("ServerIP") + ":8899/"+ method+ "/ " + userId;

        ResourceUtility.Instance.GetHttpText(url, (result) => {
            callBack.Invoke(result);
        });
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

    public void ShowChatMesage(string content)
    {
        string functionName = "ChatMessage" + "(" + content + ")";
        GetComponent<UniWebView>().EvaluateJavaScript(functionName);
    }

    public void GameOver()
    {
        string functionName = "StartGameOver()";
       
        GetComponent<UniWebView>().EvaluateJavaScript(functionName);
    }
    /// <summary>
    /// 调用网页的js函数
    /// </summary>
    /// <param name="jsFunction"></param>
    /// <param name="para"></param>
    public void CallWebPageFunction(string jsFunction, string para)
    {
        string functionName = jsFunction + "(" + para + ")";
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
