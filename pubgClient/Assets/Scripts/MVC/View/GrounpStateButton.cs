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
                    MessageBox.Show("信息提示", "确定启动游戏吗？", MessageBoxButtonState.OK, (ok) => {

                        RestFulProxy.SaveState(grounpId, (result) => {

                            result = result.Trim('"');
                            if (result.Equals("0"))
                            {
                                GetComponentInParent<RootEditGameView>().errorMessage.ShowMessage("游戏已成功启动", SoundType.None, 5.0f);
                                SwitchState();
                            }
                            else
                            {
                                GetComponentInParent<RootEditGameView>().errorMessage.ShowMessage(result, SoundType.Error, 5.0f);
                            }


                        });

                    }, true);


                   
                }
                else
                {
                    GetComponentInParent<RootEditGameView>().errorMessage.ShowMessage(grounpName + "，游戏已处于运行状态", SoundType.Error);
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
