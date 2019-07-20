using Assets.Scripts.Platform_Comm.Utils;
using command;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

namespace server
{
    public class PostEngine: IEventListener
    { 

        public void RegisterEvent()
        {
            EventMgr.Instance.AddListener(this, EventName.POST_CALLBACK);
        }
        public Dictionary<string, System.Action<string>> dic = new Dictionary<string, System.Action<string>>();

        public bool HandleEvent(string eventName, IDictionary<string, object> dictionary)
        {
            //throw new System.NotImplementedException();
            string methodName = dictionary["methodName"].ToString();
            string data = dictionary["data"].ToString();
            ProcessCallBack(methodName, data);
            return true;
        }

        public void PostData(string method, string[] parameter, System.Action<string> callBack)
        {
            if (!dic.ContainsKey(method))
            {
                dic.Add(method, callBack);
            }
            else
            {
                dic[method] = callBack;
            }

            string parametors = FormatUtil.ConnetString(new List<string>(parameter), Constant.END_SPLIT);
            string sendData = CommandName.Post.ToString() + Constant.START_SPLIT + method + Constant.END_SPLIT + parametors;
            SocketService.instance.SendData(sendData);
        }

        public void ProcessCallBack(string methodName, string data)
        {
            if (dic.ContainsKey(methodName))
            {
                dic[methodName].Invoke(data);
                dic.Remove(methodName);
            }
        }
    }
}

