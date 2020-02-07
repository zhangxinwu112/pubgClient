using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrounpSataeProxy  {

	
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
}
