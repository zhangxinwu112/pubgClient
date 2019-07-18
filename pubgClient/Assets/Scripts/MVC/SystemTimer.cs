using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemTimer
{
    private System.Timers.Timer timersTimer = null;
    public event EventHandler<EventArgs> CallBack;

    public SystemTimer()
    {
        timersTimer = new System.Timers.Timer();
        timersTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer1EventProcessor);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="time">单位为秒</param>
    public void  Start(double time = 1.0)
    {
       
        timersTimer.Interval = time*1000 ;
        timersTimer.Start();
    }
    public void Timer1EventProcessor(object source, EventArgs e)
    {

        if(CallBack != null)
        {
            CallBack(this, null);
        }
    }

  //private void RemoveEvent()
  //  {
  //      Delegate[] dels = timeCallBack.GetInvocationList();
  //      foreach (Delegate d in dels)
  //      {
  //          /*--------------------------------------*/
  //          //得到方法名
  //          object delObj = d.GetType().GetProperty("Method").GetValue(d, null);
  //          string funcName = (string)delObj.GetType().GetProperty("Name").GetValue(delObj, null);

  //          /*--------------------------------------*/
  //          timeCallBack -= d as DEL_TEST_EventHandler;
  //      }
       
  //  }

    public void StopTimer()
    {
      
        if(timersTimer!=null)
        {
            timersTimer.Stop();
            
            timersTimer.Elapsed -= new System.Timers.ElapsedEventHandler(Timer1EventProcessor);
            timersTimer = null;
        }
       
    }

    ~SystemTimer()
    {
        Debug.LogError("SystemTimer 析构函数执行");
        StopTimer();
    }
}
