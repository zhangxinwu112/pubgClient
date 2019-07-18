using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T:Component
{
    private static T _instance;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if(_instance==null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.DontSave;
                    _instance = (T)obj.AddComponent(typeof(T));
                }
            }
            return _instance;

        }
    }



}
