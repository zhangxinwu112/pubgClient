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

    public List<Grounp> grounpList = null;
    /// <summary>
    /// 创建list列表
    /// </summary>
    /// <param name="rooms"></param>
    public void CreateList<T>(List<T> datas, int type=0) where T : ModelBase
    {
       
        if((datas == null || datas.Count == 0)&& (type==1))
        {
            return;
        }

        ListMsg listMsg = null;
        //room列表
        if(type==0)
        {
            listMsg = room.GetComponentInChildren<ListMsg>();
          
           // ClearAll();
        }
        //grounp列表
        else if(type == 1)
        {
            listMsg = grounp.GetComponentInChildren<ListMsg>();
            grounpList = datas as List<Grounp>;
        }
        //userlist
        else if(type == 2)
        {
            listMsg = userList.GetComponentInChildren<ListMsg>();
         
           // ClearUserData();
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
                    if(GetComponentInParent<RootCreateRoomView>()!=null)
                    {
                        GetComponentInParent<RootCreateRoomView>().roomEditView.ShowRoomName(item.name);
                    }
                   
                }
                //group列表
                else if(type == 1)
                {
                    selectGrounpId = item.id.ToString();
                    if(GetComponentInParent<RootCreateRoomView>()!=null)
                    {
                        GetComponentInParent<RootCreateRoomView>().roomEditView.ShowGrounpName(item.name);

                        Grounp grounp = FindGrounp(selectGrounpId);
                        if (grounp != null)
                        {
                            GetComponentInParent<RootCreateRoomView>().roomEditView.ShowCheckCode(grounp.checkCode);
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


    public Grounp FindGrounp(string id)
    {
        if(grounpList!=null && grounpList.Count>0)
        {
           Grounp grounp =  grounpList.Find(c => c.id.ToString().Equals(id));//返回指定条件的元素
            return grounp;
        }

        return null;
    }
 

}
