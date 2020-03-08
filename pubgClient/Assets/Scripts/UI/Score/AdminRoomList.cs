using DataModel;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminRoomList : MonoBehaviour {

    // Use this for initialization
    private Dropdown dropList;
	void Start () {
        transform.localScale = Vector3.zero;
        return;
        dropList = GetComponentInChildren<Dropdown>();
        //玩家
        if (LoginInfo.Userinfo.type == 0)
        {
            transform.localScale = Vector3.zero;
        }
        else
        {
            CreateList();
        }
	}

    private List<Room> roomList;
    private void CreateList()
    {
        RestFulProxy.GetRoomListByAdmin(LoginInfo.Userinfo.id, (result) => {

            List<Room> roomList = Utils.CollectionsConvert.ToObject<List<Room>>(result);
            if (dropList == null || dropList.options == null)
            {
                return;
            }
            ClearData();
            this.roomList = roomList;
            DropDownDataItem optiondata = new DropDownDataItem();
            optiondata.id = -1;
            optiondata.text = "请选择战队";
            dropList.options.Add(optiondata);

            roomList.ForEach((room) => {
                optiondata = new DropDownDataItem();
                optiondata.id = room.id;
                optiondata.text = room.name;
                dropList.options.Add(optiondata);

            });
            dropList.value = -1;
        });
    }

    public int GetIndex()
    {
        return dropList.value;
    }

    public int GetRoomId()
    {
        if(dropList.value != 0 && roomList != null && roomList.Count > 0)
        {
            return  roomList[dropList.value - 1].id;
        }

        return -1;
    }

    //public void ChangeSelected()
    //{
    //    GetComponentInParent<ScoreView>().DeleteCreateList();
    //    string selectName = transform.parent.Find("upButton").GetComponent<ScoreToggle>().selectName;
    //    if (dropList.value!=0 && selectName.Equals("0") && roomList!=null && roomList.Count>0)
    //    {
    //        int roomId = roomList[dropList.value-1].id;
    //        RestFulProxy.SearchScoreByRoom(roomId, (result) => {
               
    //            List<Score> list = Utils.CollectionsConvert.ToObject<List<Score>>(result);

    //            GetComponentInParent<ScoreView>().Create(list);

    //        });
    //    }
    //}
	
    public void ClearData()
    {
        if (dropList != null && dropList.options != null)
        {
            dropList.options.Clear();
        }
    }
	
}
