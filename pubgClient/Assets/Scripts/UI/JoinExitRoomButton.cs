using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinExitRoomButton : MonoBehaviour {

    public static JoinExitRoomButton instance;

    private void Awake()
    {
        instance = this;
    }
    void Start () {
       
        UpdateButonState();
    }
	
	
    public void UpdateButonState()
    {

        RestFulProxy.GetLeaderAuthority((result) => {

            if(result.Equals("0"))
            {
                SetButtonState(true);
            }
            else
            {
                SetButtonState(false);
            }

        });
    }

    private void SetButtonState(bool isEnable)
    {
        Button[] bs = GetComponentsInChildren<Button>();
        for(int  i=0;i< bs.Length;i++)
        {
            bs[i].interactable = isEnable;
        }
    }
}
