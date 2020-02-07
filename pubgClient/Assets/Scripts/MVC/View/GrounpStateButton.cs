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
                    GrounpStateProxy.SaveState(grounpId, (result) => {

                        result = result.Trim('"');
                        if (result.Equals("0"))
                        {
                            GetComponentInParent<RootCreateRoomView>().errorMessage.ShowMessage(grounpName + "，游戏已成功启动", 5.0f);
                            SwitchState();
                        }
                        else
                        {
                            GetComponentInParent<RootCreateRoomView>().errorMessage.ShowMessage(result,5.0f);
                        }
                       

                    });
                }
                else
                {
                    GetComponentInParent<RootCreateRoomView>().errorMessage.ShowMessage(grounpName + "，游戏已处于运行状态");
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
