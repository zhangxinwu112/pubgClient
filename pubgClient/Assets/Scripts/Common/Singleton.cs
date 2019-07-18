using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T:new() 
{

    static T _intance;
    static object _lock = new object();
    public static T Instance
    {
        get
        {
            if (_intance == null)
            {
                lock (_lock)
                {
                    if (_intance == null)
                    {
                        _intance = new T();
                    }
                }
            }

            return _intance;
        }
    }

}
