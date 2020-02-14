using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStateButton : MonoBehaviour {


    [SerializeField]
    private Sprite setConfig;

    public void Change()
    {
        GetComponent<Image>().sprite = setConfig;
    }

}
