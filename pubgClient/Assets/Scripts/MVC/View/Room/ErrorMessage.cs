using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ErrorMessage : MonoBehaviour {

	void Start () {

        GetComponentInChildren<Text>().text = "";
	}
	
    public void ShowMessage(string text)
    {
        GetComponentInChildren<Text>().text = text;
        DOVirtual.DelayedCall(3.0f, ()=> {
            GetComponentInChildren<Text>().text = "";
        });
    }
}
