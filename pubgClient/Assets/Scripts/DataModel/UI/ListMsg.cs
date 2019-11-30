using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListMsg : MonoBehaviour {


    public Transform item;

	
    public  void Create(string id,string name,bool isSelected =false)
    {
        GameObject coloneItem =  GameObject.Instantiate<GameObject>(item.gameObject);
        coloneItem.transform.SetParent(item.transform.parent);
        coloneItem.transform.localScale = Vector3.one;
        coloneItem.transform.rotation = Quaternion.identity;
        coloneItem.name = id.ToString();
        coloneItem.SetActive(true);
        coloneItem.transform.Find("name").GetComponent<Text>().text = name;
        coloneItem.GetComponent<Toggle>().isOn = isSelected;
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
            GetComponentInParent<RootRoomView>().roomEditView.ShowRoomName(itemName);
            GetComponentInParent<RootRoomView>().CallSearchSingleRoomAction(roomId);


        }
       
    }

    /// <summary>
    /// 选择队
    /// </summary>
    /// <param name="toggle"></param>
    public void GrounpToggleChange(Toggle toggle)
    {
        if (toggle.isOn)
        {
            string grounpId = toggle.name;
            GetComponentInParent<RoomListView>().selectGrounpId = grounpId;
            string itemName = toggle.transform.Find("name").GetComponent<Text>().text;
            GetComponentInParent<RootRoomView>().roomEditView.ShowGrounpName(itemName);

            Grounp grounp = GetComponentInParent<RootRoomView>().roomListView.FindGrounp(grounpId);
            if(grounp!=null)
            {
                GetComponentInParent<RootRoomView>().roomEditView.ShowPassword(grounp.checkCode);
            }
            GetComponentInParent<RootRoomView>().CallSearchSingleGrounpAction(grounpId);


        }
    }
}
