using DataModel;
using Model;
using PureMVC.Patterns;
using server.Model;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class GrounpEditProxy : Proxy
{
    public new const string NAME = "GrounpEditProxy";

    public GrounpEditProxy() : base(NAME)
    {

    }

    public void AddGrounp(string grounpName,string playerTime)
    {
        if(SocketService.instance==null)
        {
            Debug.LogError("SocketService is null");
            return;
        }
        SocketService.instance.PostData("server.DAO.EditGrounpDao" + Constant.METHOD_SPLIT+ "AddGrounp", new string[] { grounpName, playerTime,LoginInfo.Userinfo.id.ToString(),"cs" }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            
            if(dataResult!=null)
            {
                SendNotification(RoomNotifications.CREATE_GROUNP_RESULT, dataResult);
            }
     
        });
    }


    public void EditGrounp(string grounpName, string playerTime,string roomName,string checkCode, string grounpId,string roomId)
    {
        if (SocketService.instance == null)
        {
            Debug.LogError("SocketService is null");
            return;
        }
        SocketService.instance.PostData("server.DAO.EditGrounpDao" + Constant.METHOD_SPLIT + "UpdateGrounp", new string[] { grounpName, playerTime,roomName, checkCode, grounpId, roomId }, (result) => {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            SendNotification(RoomNotifications.EDIT_GROUNP_RESULT, dataResult);

        });
    }

    public void DeleteGrounp(string  grounpId)
    {
        SocketService.instance.PostData("server.DAO.EditGrounpDao" + Constant.METHOD_SPLIT + "DeleteGrounp", new string[] { grounpId }, (result) =>
        {
            DataResult dataResult = Utils.CollectionsConvert.ToObject<DataResult>(result);
            SendNotification(RoomNotifications.DELETE_GROUNP_RESULT, dataResult);

        });
    }



}
