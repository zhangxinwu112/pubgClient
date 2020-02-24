using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreToggle : MonoBehaviour {

    [HideInInspector]
    public string selectName = "0";
    public void TooglePlayer(Toggle toggle)
    {
        if(toggle.isOn)
        {
            selectName = toggle.name;
            GetComponentInParent<ScoreView>().DeleteCreateList();
            //玩家
            if (LoginInfo.Userinfo.type == 0)
            {
                GetComponentInParent<ScoreView>().RequstData(int.Parse(toggle.name));
            }
            else if(LoginInfo.Userinfo.type == 1)
            {
               
                if(selectName.Equals("0"))
                {
                    AdminRoomList ar = transform.parent.GetComponentInChildren<AdminRoomList>();
                    if (ar.GetIndex() != 0)
                    {
                        int roomId = ar.GetRoomId();
                        RestFulProxy.SearchScoreByRoom(roomId, (result) => {
              
                            List<Score> list = Utils.CollectionsConvert.ToObject<List<Score>>(result);

                            GetComponentInParent<ScoreView>().Create(list);

                        });
                    }
                }
                else
                {
                    GetComponentInParent<ScoreView>().RequstData(int.Parse(toggle.name));
                }
               
            }
        }
    }


    public void ToogleCurrent(Toggle toggle)
    {

    }
}
