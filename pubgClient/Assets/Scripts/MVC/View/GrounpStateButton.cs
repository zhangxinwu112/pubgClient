using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrounpStateButton : MonoBehaviour {

    [SerializeField]
    private Sprite StopSprite;

    [SerializeField]
    private Sprite PlaySprite;

    public bool isPlay = false;
	void Start () {

        GetComponent<Button>().onClick.AddListener(() => {
            SwitchState();

        });
	}

    public void SwitchState()
    {
        if(!isPlay)
        {
            isPlay = !isPlay;

            GetComponent<Image>().sprite = PlaySprite;
        }
    }
	
	
}
