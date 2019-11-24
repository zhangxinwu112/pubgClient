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
	
    /// <summary>
    /// 创建房间列表
    /// </summary>
    /// <param name="rooms"></param>
    public void CreateRoomList(List<Room> rooms)
    {
        ListMsg listMsg = room.GetComponentInChildren<ListMsg>();
        listMsg.Clear();
        int i = 0;
        rooms.ForEach((item) => {

            if(i==0)
            {
                listMsg.Create(item.id.ToString(), item.name,true);
            }
            else
            {
                listMsg.Create(item.id.ToString(), item.name, false);
            }
            
            i++;
        });
    }

   
}
