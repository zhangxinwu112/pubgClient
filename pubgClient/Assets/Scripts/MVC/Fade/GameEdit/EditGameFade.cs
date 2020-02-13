using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditGameFade : CommonRoomFade
{

    private static EditGameFade instance = null;

    public static EditGameFade GetInstance()
    {
        if (instance == null)
            instance = new EditGameFade();
        return instance;
    }

    protected override void InitializeController()
    {
        base.InitializeController();

        InitializeCommand();
    }

    protected override void InitializeCommand()
    {
        base.InitializeCommand();
        RegisterCommand(RoomNotifications.CREATE_GROUNP, typeof(CreateGrounpCommand));
        RegisterCommand(RoomNotifications.EDIT_GROUNP, typeof(EditGrounpCommand));
        RegisterCommand(RoomNotifications.DELETE_GROUNP, typeof(DeleteGrounpCommand));
    }

    protected override void InitializeModel()
    {
        base.InitializeModel();
        InitializeProxy();
    }
    protected override void InitializeProxy()
    {
        base.InitializeProxy();
        RegisterProxy(new GrounpEditProxy());
    }

    private EditGameMeditor editGameMeditor;

    public void StartUp(GameObject root)
    {
        if(editGameMeditor == null)
        {
            editGameMeditor = new EditGameMeditor();
            RegisterMediator(editGameMeditor);
        }
        editGameMeditor.Init(root);
    }

    public void DestroyEvent()
    {
        if (editGameMeditor != null)
        {
            RemoveMediator(EditGameMeditor.NAME);
            editGameMeditor.RemoveEvent();
            editGameMeditor = null;
        }
    }

   
}
