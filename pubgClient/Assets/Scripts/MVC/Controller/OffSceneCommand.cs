using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class OffSceneCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        Application.Quit();
    }
}
