using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageShow : MonoBehaviour {

    public static MessageShow instance;

    private void Awake()
    {
        instance = this;
        HideMessage();
    }
    public void ShowMesage(string text)
    {

        transform.localScale = Vector3.one;
        transform.GetComponentInChildren<Text>().text = text;
    }

    public void HideMessage()
    {
        transform.localScale = Vector3.zero;
        transform.GetComponentInChildren<Text>().text = "";
        
    }
}
