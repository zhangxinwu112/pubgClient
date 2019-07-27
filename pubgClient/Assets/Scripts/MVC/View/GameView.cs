using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameView : MonoBehaviour {

    public Toggle map2d;
    public Toggle map3d;

    public Toggle chat;
    public Toggle set;

    public ShowMapPoint showMapPoint;

    public Text titleText;
    void Start () {
        GameFade.GetInstance().StartUp(gameObject);
    }

    public void OnClickMap2dButton(UnityAction<int,string> action)
    {

        EventTriggerListener.Get(map2d.transform).onClick += () =>
        {
            if(map2d.isOn)
            {
                action.Invoke(0, map2d.GetComponentInChildren<Text>().text);
            }
            
        };
      
    }

    public void OnClickMap3dButton(UnityAction<int, string> action)
    {
        EventTriggerListener.Get(map3d.transform).onClick += () =>
        {
            if (map3d.isOn)
            {
                action.Invoke(1, map3d.GetComponentInChildren<Text>().text);
            }

        };

    }

    public void OnClickChatButton(UnityAction<int, string> action)
    {
        EventTriggerListener.Get(chat.transform).onClick += () =>
        {
            if (chat.isOn)
            {
                action.Invoke(2, chat.GetComponentInChildren<Text>().text);
            }

        };

    }


    public void OnClickSetButton(UnityAction<int, string> action)
    {
        EventTriggerListener.Get(set.transform).onClick += () =>
        {
            if (set.isOn)
            {
                action.Invoke(3, set.GetComponentInChildren<Text>().text);
            }

        };

    }

    public void SetTitle(string value)
    {
        titleText.text = value;
    }
}
