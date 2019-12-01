using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    void Start () {

        //PlayerPrefs.DeleteAll();
        Config.Startload(this,
           () =>
           {
                //不删除
                DontDestroyOnLoad(gameObject);
                //添加脚本
                InitScripts intiScripts = gameObject.AddComponent<InitScripts>();
                //调用跳转场景方法
                intiScripts.Initialized();
                //跳转场景 到 start场景
                SceneTools.instance.LoadScene("Login");
                //将打印
                Application.logMessageReceived += Handler;
           });


        
    }

    private void Handler(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            Debug.LogError("系统出错" + stackTrace);
        }
    }

}
