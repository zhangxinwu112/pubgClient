using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ErrorMessage : MonoBehaviour {

	void Start () {

        GetComponentInChildren<Text>().text = "";
	}
	
    public void ShowMessage(string text,float time = 3.0f)
    {
        GetComponentInChildren<Text>().text = text;
        DOVirtual.DelayedCall(time, ()=> {
            GetComponentInChildren<Text>().text = "";
        });
    }
}
