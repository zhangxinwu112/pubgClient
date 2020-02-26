using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DataModel;

/// <summary>
/// 资源加载
/// </summary>
public class ResourceUtility : MonoSingleton<ResourceUtility>
{
   
    private  void GetHttpResource(string url, Action<WWW> action)
    {
        StartCoroutine(GetHttpResourceC(url, action));
    }
        
    private  IEnumerator GetHttpResourceC(string url, Action<WWW> action)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError("下载错误，请检查路径：" + url + "==原因：" + www.error);
            //NGUIDebug.Log("下载错误，请检查路径：" + url + "==原因：" + www.error);
            action(www);
        }
           
        else
        {
            action(www);
        }
            
    }

    public  void GetHttpText(string url, Action<string> action)
    {
        GetHttpResource(url, (wwww) => {

            //对结果进行转义移除，和空处理
            string result = FormatStr(wwww.text).Trim();
            if (!string.IsNullOrEmpty(result) && !result.Equals("[]"))
            {
                action.Invoke(result);
            }
        }
        
        );
    }

    private string FormatStr(string result)
    {
        result = result.Trim('"');
        result = result.Replace("\\", "");

        return result;
    }

    public  void GetHttpTexture(string url, Action<Texture> action)
    {
        GetHttpResource(url, www => action(www.texture));
    }

    public  void GetHttpAssetBundle(string url, Action<AssetBundle> action)
    {
        GetHttpResource(url, www => action(www.assetBundle));
    }

    public void GetHttpAssetBundle(string url,Action<GameObject> action, string code)
    {
        GetHttpResource(url, www => action(www.assetBundle.LoadAsset<GameObject>(code)));
    }

    public  void GetHttpAudio(string url,Action<AudioClip> action)
    {
        GetHttpResource(url, www => action(www.GetAudioClip()));
    }

    public  T GetResource<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load(path) as T;
    }
}