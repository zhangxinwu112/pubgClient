using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkCheck : MonoBehaviour
{
    void Update()
    {
       
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            NoNetWorkSingle.Instance.ShowNoNetWork();
        }
        else
        {
            NoNetWorkSingle.Instance.HideNoNetWork();
        }
        
    }
}


