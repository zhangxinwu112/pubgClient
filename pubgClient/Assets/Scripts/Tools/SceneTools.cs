using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//加载场景

public  class SceneTools : MonoBehaviour {

    public static SceneTools instance;
    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(string sceneName, System.Action callBack = null)
    {
        PlayerPrefs.SetString("currrentScene", sceneName);
        StartCoroutine(AsyncLoadScene(sceneName, callBack));

    }

    public IEnumerator AsyncLoadScene(string sceneName,System.Action callBack)
    {
        AsyncOperation asyncOperation  = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        if (asyncOperation == null)
        {
            Debug.LogError("场景加载出错，请检查：" + sceneName );
            if (callBack != null)
            {
                callBack();
            }
            yield break;
        }
        asyncOperation.allowSceneActivation = true;
        while (true)
        {
            if (asyncOperation.isDone)
            {              
                if (callBack != null)
                {
                    callBack();
                }
                break;
            }
            yield return 0;
        }

    }

    public void BackScene()
    {
        if (LoginInfo.Userinfo.type == 0)
        {
            SceneTools.instance.LoadScene("JoinRoom");
        }
        else
        {
            SceneTools.instance.LoadScene("EditGame");
        }
    }
}
