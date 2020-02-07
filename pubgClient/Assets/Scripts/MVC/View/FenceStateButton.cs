using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FenceStateButton : MonoBehaviour {


    [SerializeField]
    private Sprite setConfig;
	void Start () {
		
	}

    public void Change()
    {
        GetComponent<Image>().sprite = setConfig;
    }

}
