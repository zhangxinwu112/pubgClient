using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSInit : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(StartGPS());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator StartGPS()
    {
        // Input.location 用于访问设备的位置属性（手持设备）, 静态的LocationService位置
        // LocationService.isEnabledByUser 用户设置里的定位服务是否启用
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }

        // LocationService.Start() 启动位置服务的更新,最后一个位置坐标会被使用
        Input.location.Start(10.0f, 10.0f);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            // 暂停协同程序的执行(1秒)
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            
            yield break;
        }
        //else
        //{
        //    this.gps_info = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;
        //    this.gps_info = this.gps_info + " Time:" + Input.location.lastData.timestamp;
        //    yield return new WaitForSeconds(100);
        //}
    }
}
