using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameMeditor : Mediator
{
    public new const string NAME = "GameMeditor";

    private GameObject root = null;

    public GameMeditor(GameObject _root) : base(NAME)
    {
        this.root = _root;

        this.root.GetComponent<GameView>().OnClickMap2dButton(OnClick2dMap);
        this.root.GetComponent<GameView>().OnClickMap3dButton(OnClick3dMap);
        this.root.GetComponent<GameView>().OnClickChatButton(OnClickChatButton);
        this.root.GetComponent<GameView>().OnClickSetButton(OnClickSetButton);
    }

    private int currentIndex = 0;
    private void OnClick2dMap(int _currentIndex,string textTitle)
    {
        if(currentIndex ==_currentIndex)
        {
            return;
        }
        string mapUrl = Config.parse("Map2dAddress");
        this.root.GetComponent<GameView>().showMapPoint.DeleteWebPage();
        DOVirtual.DelayedCall(0.1f, () => {

            this.root.GetComponent<GameView>().showMapPoint.OpenWebPage(mapUrl);
        });
        SetValue(_currentIndex, textTitle);
    }

    private void OnClick3dMap(int _currentIndex, string textTitle)
    {
        if (currentIndex == _currentIndex)
        {
            return;
        }
        string mapUrl = Config.parse("Map3dAddress");
        this.root.GetComponent<GameView>().showMapPoint.DeleteWebPage();
        DOVirtual.DelayedCall(0.1f, () => {

            this.root.GetComponent<GameView>().showMapPoint.OpenWebPage(mapUrl);
        });

        SetValue(_currentIndex, textTitle);
    }

    private void OnClickChatButton(int _currentIndex, string textTitle)
    {
        if (currentIndex == _currentIndex)
        {
            return;
        }

        SetValue(_currentIndex, textTitle);
    }

    private void OnClickSetButton(int _currentIndex, string textTitle)
    {
        if (currentIndex == _currentIndex)
        {
            return;
        }

        SetValue(_currentIndex, textTitle);
    }

    private void SetValue(int _currentIndex, string textTitle)
    {
        this.root.GetComponent<GameView>().SetTitle(textTitle);
        currentIndex = _currentIndex;

        UIUtility.LockScreen();
    }

    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
      
        return list;
    }
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {

            default:
                break;
        }
    }
 }
