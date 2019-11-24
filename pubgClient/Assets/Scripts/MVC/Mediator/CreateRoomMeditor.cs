using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateRoomMeditor : Mediator
{
    public new const string NAME = "CreateRoomMeditor";

    private GameObject root = null;

    public CreateRoomMeditor(GameObject _root) : base(NAME)
    {
        this.root = _root;

        
    }

   
 }
