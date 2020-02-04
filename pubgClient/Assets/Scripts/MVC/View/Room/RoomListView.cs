using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListView : MonoBehaviour {

    // Use this for initialization
    public Transform _grounp;

    public Transform _room;

    public Transform userList;

  
    public string selectRoomId = "";

    public string selectGrounpId = "";

    public List<Room> roomList = null;

    public List<Grounp> grounpList = null;
    /// <summary>
    /// 创建list列表
    /// </summary>
    /// <param name="rooms"></param>
    public void CreateList<T>(List<T> datas, int type=0) where T : ModelBase
    {
       
        if((datas == null || datas.Count == 0))
        {
            if(type == 0)
            {
                ClearAll();
            }
            else if(type == 1)
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

      
        ListMsg listMsg = null;
        //grounp列表
        if (type==0)
        {
            listMsg = _grounp.GetComponentInChildren<ListMsg>();
            grounpList = datas as List<Grounp>;


        }
        //room列表
        else if(type == 1)
        {
            listMsg = _room.GetComponentInChildren<ListMsg>();
           
            roomList = datas as List<Room>;
        }
        //userlist
        else if(type == 2)
        {
            listMsg = userList.GetComponentInChildren<ListMsg>();
          
           
        }
        listMsg.Clear();
        if (datas==null || datas.Count==0)
        {
            return;
        }
        int i = 0;
        datas.ForEach((item) => {

            if(i==0)
            {
                listMsg.Create(item.id.ToString(), item.name,true);
                if(type == 0)
                {
                    selectRoomId = item.id.ToString();
                    if(GetComponentInParent<RootCreateRoomView>()!=null)
                    {
                        string playerTime = (item as Grounp).playerTime.ToString();
                        GetComponentInParent<RootCreateRoomView>().roomEditView.ShowRoom(item.name, playerTime);
                    }
                   
                }
                //room列表
                else if(type == 1)
                {
                    selectGrounpId = item.id.ToString();
                    if(GetComponentInParent<RootCreateRoomView>()!=null)
                    {
                        GetComponentInParent<RootCreateRoomView>().roomEditView.ShowGrounpName(item.name);

                        Room room = FindRoomByKey(selectGrounpId);
                        if (room != null)
                        {
                            GetComponentInParent<RootCreateRoomView>().roomEditView.ShowCheckCode(room.checkCode);
                        }
                    }
                   
                }
            }
            else
            {
                listMsg.Create(item.id.ToString(), item.name, false);
            }
            
            i++;
        });
    }

   

    private void ClearUserData()
    {
        ListMsg listMsg = userList.GetComponentInChildren<ListMsg>();
        if(listMsg!=null)
        {
            listMsg.Clear();
        }
    }

    private void ClearAll()
    {
        ListMsg[]  listmsgs =  userList.transform.parent.GetComponentsInChildren<ListMsg>();
        for(int i=0;i< listmsgs.Length;i++)
        {
            listmsgs[i].Clear();
        }
    }


    public Room FindRoomByKey(string id)
    {
        if(roomList!=null && roomList.Count>0)
        {
           Room room = roomList.Find(c => c.id.ToString().Equals(id));//返回指定条件的元素
            return room;
        }

        return null;
    }

    public Grounp FindGrounpByKey(string id)
    {
        if (grounpList != null && grounpList.Count > 0)
        {
            Grounp grounp = grounpList.Find(c => c.id.ToString().Equals(id));//返回指定条件的元素
            return grounp;
        }

        return null;
    }


}
