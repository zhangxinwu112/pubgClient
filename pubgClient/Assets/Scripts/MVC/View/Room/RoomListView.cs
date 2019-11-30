using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListView : MonoBehaviour {

    // Use this for initialization
    public Transform room;

    public Transform grounp;

    public Transform userList;

  
    public string selectRoomId = "";

    public string selectGrounpId = "";


    /// <summary>
    /// 创建list列表
    /// </summary>
    /// <param name="rooms"></param>
    public void CreateList<T>(List<T> datas, int type=0) where T : ModelBase
    {
       
        ListMsg listMsg = null;
        //room列表
        if(type==0)
        {
            listMsg = room.GetComponentInChildren<ListMsg>();
          
            ClearAll();
        }
        //grounp列表
        else if(type == 1)
        {
            listMsg = grounp.GetComponentInChildren<ListMsg>();
        }
        //userlist
        else if(type == 2)
        {
            listMsg = userList.GetComponentInChildren<ListMsg>();
            ClearUserData();
        }
        listMsg.Clear();
        if(datas==null || datas.Count==0)
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
                    GetComponentInParent<RootRoomView>().roomEditView.ShowRoomName(item.name);
                }
                //group列表
                else if(type == 1)
                {
                    selectGrounpId = item.id.ToString();
                    GetComponentInParent<RootRoomView>().roomEditView.ShowGrounpName(item.name);
                    if(item is Grounp)
                    {
                        Grounp grounp = item as Grounp;
                        GetComponentInParent<RootRoomView>().roomEditView.ShowPassword(grounp.checkCode);
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
 

}
