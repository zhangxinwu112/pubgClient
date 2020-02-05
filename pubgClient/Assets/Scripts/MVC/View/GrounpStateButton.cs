using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrounpStateButton : MonoBehaviour {

    [SerializeField]
    private bool isEdit = true;
    [SerializeField]
    private Sprite StopSprite;

    [SerializeField]
    private Sprite PlaySprite;

  
    public string grounpId;
    public string grounpName;

    public bool isPlay = false;
	void Start () {

        if(isEdit)
        {
            GetComponent<Button>().onClick.AddListener(() => {
                if (!isPlay)
                {
                    GrounpSataeProxy.SaveState(grounpId, (result) => {

                        GetComponentInParent<RootCreateRoomView>().errorMessage.ShowMessage(grounpName + "，游戏已启动", 5.0f);
                        SwitchState();

                    });
                }
                else
                {
                    GetComponentInParent<RootCreateRoomView>().errorMessage.ShowMessage(grounpName + "，游戏已处于运行状态", 3.0f);
                }


            });
        }
        

       // GetComponent<Button>().interactable = isEdit;

    }

    public void SwitchState()
    {
        isPlay = !isPlay;
        GetComponent<Image>().sprite = PlaySprite;

    }

   
}
