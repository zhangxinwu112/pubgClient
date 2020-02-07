using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListMsg : MonoBehaviour {

    public Transform item;
    public  void Create(string id,string name,bool isSelected,int runState=-1,int userCount=0,bool isCurrentUser =false,bool isDefence = false)
    {
        GameObject coloneItem =  GameObject.Instantiate<GameObject>(item.gameObject);
        coloneItem.transform.SetParent(item.transform.parent);
        coloneItem.transform.localScale = Vector3.one;
        coloneItem.transform.rotation = Quaternion.identity;
        coloneItem.name = id.ToString();
        coloneItem.SetActive(true);
        coloneItem.transform.Find("name").GetComponent<Text>().text = name;
        coloneItem.GetComponent<Toggle>().isOn = isSelected;

        //处理grounp
        if(coloneItem.transform.Find("GrounpState")!=null)
        {
            if(runState == 0)
            {
                coloneItem.transform.Find("GrounpState").GetComponent<GrounpStateButton>().SwitchState();
            }

            coloneItem.transform.Find("GrounpState").GetComponent<GrounpStateButton>().grounpId = id;
            coloneItem.transform.Find("GrounpState").GetComponent<GrounpStateButton>().grounpName = name;
        }
        if(coloneItem.transform.Find("SetFence") != null)
        {
            if(isDefence)
            {
                coloneItem.transform.Find("SetFence").GetComponent<FenceStateButton>().Change();
            }
        }

        if(coloneItem.transform.Find("count") != null)
        {
            coloneItem.transform.Find("count").GetComponentInChildren<Text>().text = userCount.ToString();
            if(isCurrentUser)
            {
                coloneItem.transform.Find("count").GetComponent<Image>().color = new Color32(204, 51, 255, 255);
            }
        }
    }

    private readonly string ItemName = "item";
    public void Clear()
    {
        foreach(Transform child in item.transform.parent)
        {
            if(!child.name.Equals(ItemName))
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    /// <summary>
    /// 选择房间
    /// </summary>
    /// <param name="toggle"></param>
    public void RoomToggleChange(Toggle toggle)
    {
        if(toggle.isOn)
        {
            string roomId = toggle.name;
            GetComponentInParent<RoomListView>().selectRoomId = roomId;
            string itemName = toggle.transform.Find("name").GetComponent<Text>().text;
            if(GetComponentInParent<RootCreateRoomView>()!=null)
            {

                string playerTime = GetComponentInParent<RoomListView>().FindGrounpByKey(roomId)==null?"": 
                    GetComponentInParent<RoomListView>().FindGrounpByKey(roomId).playerTime.ToString();
                GetComponentInParent<RootCreateRoomView>().roomEditView.ShowRoom(itemName, playerTime);
               
            }
            GetComponentInParent<RootBaseRoomView>().CallSearchSingleRoomAction(roomId);

        }
       
    }

    /// <summary>
    /// 选择分队
    /// </summary>
    /// <param name="toggle"></param>
    public void GrounpToggleChange(Toggle toggle)
    {
        if (toggle.isOn)
        {
            string grounpId = toggle.name;
            GetComponentInParent<RoomListView>().selectGrounpId = grounpId;
            string itemName = toggle.transform.Find("name").GetComponent<Text>().text;
            if(GetComponentInParent<RootCreateRoomView>()!=null)
            {
                GetComponentInParent<RootCreateRoomView>().roomEditView.ShowGrounpName(itemName);

                Room room = GetComponentInParent<RootCreateRoomView>().roomListView.FindRoomByKey(grounpId);
                if (room != null)
                {
                    GetComponentInParent<RootCreateRoomView>().roomEditView.ShowCheckCode(room.checkCode);
                }
            }
            GetComponentInParent<RootBaseRoomView>().CallSearchSingleGrounpAction(grounpId);
        }
    }
}
