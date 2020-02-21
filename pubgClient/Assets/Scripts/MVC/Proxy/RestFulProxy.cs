using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestFulProxy
{

	
    public static void  SaveState(string grounpid,System.Action<string> scuessCallBack,System.Action<string> failtureCallBack = null)
    {
        string url = "http://"+ Config.parse("ServerIP")+ ":8899/UpdateGrounpState/" + grounpid;
        ResourceUtility.Instance.GetHttpText(url, scuessCallBack);
    }


    public static void SearchState(string grounpid, System.Action<string> scuessCallBack, System.Action<string> failtureCallBack = null)
    {
        string url = "http://" + Config.parse("ServerIP") + ":8899/SearchGrounpState/" + grounpid;
        ResourceUtility.Instance.GetHttpText(url, scuessCallBack);
    }

    public static void Debug(string content, System.Action<string> scuessCallBack, System.Action<string> failtureCallBack = null)
    {
        return;
        //string url = "http://" + Config.parse("ServerIP") + ":8899/Deubg/" + content;
        //ResourceUtility.Instance.GetHttpText(url, scuessCallBack);
    }

    public static void SetUserState(int userId, System.Action<string> scuessCallBack, System.Action<string> failtureCallBack = null)
    {
        string url = "http://" + Config.parse("ServerIP") + ":8899/SetPlayerState/" + userId;
       // NGUIDebug.Log(url);
        ResourceUtility.Instance.GetHttpText(url, scuessCallBack);
    }

    public static void CheckEnterButton(System.Action<string> scuessCallBack, System.Action<string> failtureCallBack = null)
    {

        int userId = LoginInfo.Userinfo.id;
        int userType = LoginInfo.Userinfo.type;
        string url = "http://" + Config.parse("ServerIP") + ":8899/CheckEnterButton/" + userId+"|"+ userType;
        // NGUIDebug.Log(url);
        ResourceUtility.Instance.GetHttpText(url, scuessCallBack);
    }


}
