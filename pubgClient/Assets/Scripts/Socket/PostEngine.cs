using Assets.Scripts.Platform_Comm.Utils;
using command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEngine  {

    public Dictionary<string, System.Action<string>> dic = new Dictionary<string, System.Action<string>>();
    public void PostData(string method, string[] parameter, System.Action<string> callBack)
    {
        //if(!dic.ContainsKey(method))
        //{
            //dic.Add(method, callBack);

            string parametors = FormatUtil.ConnetString(new List<string>(parameter), Constant.SPIT_CHAR);
            string sendData = CommandName.Post.ToString() + " " + method + " " + parametors;
            Debug.Log(sendData);
            SocketService.instance.SendData(sendData);
        //}
    }
}
