using DataModel;
using server.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListView : MonoBehaviour {

    // Use this for initialization
    public Transform _grounp;

    public Transform _room;

    public Transform userList;

  
    public string selectGameId = "";

    public string selectRoomId = "";

    public List<Room> roomList = null;

    public List<Grounp> grounpList = null;
    private ListMsg listMsg = null;
    /// <summary>
    /// 创建list列表
    /// </summary>
    /// <param name="rooms"></param>
    public void CreateList<T>(List<T> datas, int type=0) where T : ModelBase
    {
        if ((datas == null || datas.Count == 0))
        {
            if (type == 0)
            {
                GetComponentInParent<RootEditGameView>().SetButtonState(false);
                ClearAll();
            }
            else if (type == 1)
            {
                _room.GetComponentInChildren<ListMsg>().Clear();
                userList.GetComponentInChildren<ListMsg>().Clear();
            }
            else
            {
                userList.GetComponentInChildren<ListMsg>().Clear();
            }
            return;
        }


        switch (type)
        {
            case 0:
                {
                    CreateGameList<T>(datas,type);
                    break;
                }
            case 1:
                {
                    CreateGrounpList<T>(datas,type);
                    break;
                }
            case 2:
                {
                    CreateUserList<T>(datas,type);
                    break;
                }
        }
       
    }

    private void CreateGameList<T>(List<T> datas, int type) where T : ModelBase
    {
        ClearAll();
        listMsg = _grounp.GetComponentInChildren<ListMsg>();
        GameObject newGameObject = null;
        int i = 0;
        List<Grounp> listGournp = datas as List<Grounp>;
       

        listGournp.ForEach((item) => {

            bool isSeleced = false;
            
            //第一个默认
            if (i == 0)
            { 
                isSeleced = true;
                selectGameId = item.id.ToString();
                if (GetComponentInParent<RootEditGameView>() != null)
                {
                    string playerTime = item.playerTime.ToString();
                    GetComponentInParent<RootEditGameView>().gameEditView.ShowEditData(item.name, item.playerTime.ToString(),
                        item.checkCode);
                    GetComponentInParent<RootEditGameView>().SetButtonState(true);
                }

            }
          
            newGameObject = listMsg.Create(item.id.ToString(), item.name, isSeleced);
            ListData.SetGameListData(listGournp);
            listMsg.SetGame(newGameObject, item.id, item.runState, item.isDefence, i);

            i++;
        });
       


    }

    private void CreateGrounpList<T>(List<T> datas, int type = 0) where T : ModelBase
    {
        listMsg = _room.GetComponentInChildren<ListMsg>();

        roomList = datas as List<Room>;
        listMsg.Clear();
        int i = 0;
        ListData.SetRoomListData(roomList);
        datas.ForEach((item) => {
            bool isSeleced = false;
            if (i == 0)
            {
                isSeleced = true;
            }
            
            GameObject newObject =  listMsg.Create(item.id.ToString(), item.name, isSeleced);
            if((item as Room).runState==0)
            {
                listMsg.SetGrountp(newObject, (item as Room).userCount, true, (item as Room).isCurrentUser);
            }
            else
            {
                listMsg.SetGrountp(newObject, (item as Room).userCount, false, (item as Room).isCurrentUser);
            }
           
            i++;
        });

    }

    private void CreateUserList<T>(List<T> datas, int type) where T : ModelBase
    {
        listMsg = userList.GetComponentInChildren<ListMsg>();
        listMsg.Clear();
        int i = 0;
        datas.ForEach((item) => {

            GameObject newObject = null;
            if (i == 0)
            {
                newObject =  listMsg.Create(item.id.ToString(), item.name, true);
            }
            else
            {
                newObject = listMsg.Create(item.id.ToString(), item.name, false);
            }
            listMsg.SetUser(newObject, item as UserItem);
            i++;
        });

    }

    private void ClearAll()
    {
        ListMsg[]  listmsgs =  userList.transform.parent.GetComponentsInChildren<ListMsg>();
        for(int i=0;i< listmsgs.Length;i++)
        {
            listmsgs[i].Clear();
        }
    }

}
