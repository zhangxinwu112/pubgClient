using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 检查网络和GPS
/// </summary>
public class DeviceCheck : MonoBehaviour {

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
       // Process.setThreadPriority(Process.THREAD_PRIORITY_BACKGROUND);
    }
    // Update is called once per frame
    void Update () {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {

            MessageShow.instance.ShowMesage("网络连接不可用");
        }
        else
        {

            //if(!Input.location.isEnabledByUser || Input.location.status == LocationServiceStatus.Failed || Input.location.status == LocationServiceStatus.Stopped)
            //{
            //    MessageShow.instance.ShowMesage("系统GPS定位服务未开启");
            //}
            //else
            //{
                MessageShow.instance.HideMessage();
           // }
        }
    }
}
