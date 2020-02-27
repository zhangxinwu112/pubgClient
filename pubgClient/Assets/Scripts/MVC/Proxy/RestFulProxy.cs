using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestFulProxy
{

	
    public static void  SaveState(string grounpid,System.Action<string> successCallBack, System.Action<string> failtureCallBack = null)
    {
        string url = "http://"+ Config.parse("ServerIP")+ ":8899/UpdateGrounpState/" + grounpid;
        ResourceUtility.Instance.GetHttpText(url, successCallBack);
    }


    public static void SearchState(string grounpid, System.Action<string> successCallBack, System.Action<string> failtureCallBack = null)
    {
        string url = "http://" + Config.parse("ServerIP") + ":8899/SearchGrounpState/" + grounpid;
        ResourceUtility.Instance.GetHttpText(url, successCallBack);
    }

    public static void Debug(string content, System.Action<string> successCallBack, System.Action<string> failtureCallBack = null)
    {
        return;
        //string url = "http://" + Config.parse("ServerIP") + ":8899/Deubg/" + content;
        //ResourceUtility.Instance.GetHttpText(url, scuessCallBack);
    }

    public static void SetUserState(int userId, System.Action<string> successCallBack, System.Action<string> failtureCallBack = null)
    {
        string url = "http://" + Config.parse("ServerIP") + ":8899/SetPlayerState/" + userId;
       // NGUIDebug.Log(url);
        ResourceUtility.Instance.GetHttpText(url, successCallBack);
    }

    public static void CheckEnterButton(System.Action<string> successCallBack, System.Action<string> failtureCallBack = null)
    {

        int userId = LoginInfo.Userinfo.id;
        int userType = LoginInfo.Userinfo.type;
        string url = "http://" + Config.parse("ServerIP") + ":8899/CheckEnterButton/" + userId+"|"+ userType;
        // NGUIDebug.Log(url);
        ResourceUtility.Instance.GetHttpText(url, successCallBack);
    }

    public static void SearchScore(int currrentUser,System.Action<string> successCallBack, System.Action<string> failtureCallBack = null)
    {
        int userId = LoginInfo.Userinfo.id;
        int userType = LoginInfo.Userinfo.type;
        string url = "http://" + Config.parse("ServerIP") + ":8899/SearchScore/" + userId + "/" + userType+"/"+currrentUser;
        // NGUIDebug.Log(url);
        ResourceUtility.Instance.GetHttpText(url, successCallBack);
    }

    public static void GetRoomListByAdmin(int adminUser, System.Action<string> successCallBack, System.Action<string> failtureCallBack = null)
    { 
        string url = "http://" + Config.parse("ServerIP") + ":8899/GetRoomList/" + adminUser;
        ResourceUtility.Instance.GetHttpText(url, successCallBack);
    }
    public static void SearchScoreByRoom(int roomId, System.Action<string> successCallBack, System.Action<string> failtureCallBack = null)
    {
        string url = "http://" + Config.parse("ServerIP") + ":8899/SearchScoreByRoom/" + roomId ;
        // NGUIDebug.Log(url);
        ResourceUtility.Instance.GetHttpText(url, successCallBack);
    }

    public static void IsEditRoom(int roomId, System.Action<string> successCallBack, System.Action<string> failtureCallBack = null)
    {
        string url = "http://" + Config.parse("ServerIP") + ":8899/EditRoom/" + roomId+"/"+LoginInfo.Userinfo.id;
        ResourceUtility.Instance.GetHttpText(url, successCallBack);
    }

    public static void GetLeaderAuthority(System.Action<string> successCallBack, System.Action<string> failtureCallBack = null)
    {
        string url = "http://" + Config.parse("ServerIP") + ":8899/GetLeaderAuthority/"+ LoginInfo.Userinfo.id;
        ResourceUtility.Instance.GetHttpText(url, successCallBack);
    }




}
