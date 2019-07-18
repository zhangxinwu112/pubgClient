using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMono : MonoSingleton<TimerMono>
{
    public System.Action callBack;

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        while(true)
        {
            if(callBack!=null)
            {
                callBack.Invoke();
            }
            yield return new WaitForSeconds(60);
        }
    }

    public   void StopTimer()
    {
        StopAllCoroutines();
    }


}
