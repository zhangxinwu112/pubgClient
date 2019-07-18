using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoNetWorkSingle : MonoSingleton<NoNetWorkSingle> {


    public static NoNetWorkSingle instance;
    private void Awake()
    {
        transform.localScale = Vector3.zero;
        instance = this;
    }

    public void ShowNoNetWork()
    {
        transform.localScale = Vector3.one;
        transform.SetAsLastSibling();
    }

    public void HideNoNetWork()
    {
        transform.localScale = Vector3.zero;

    }
}
