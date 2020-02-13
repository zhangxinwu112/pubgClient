using DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListMsg : MonoBehaviour {

    public Transform item;
    public  GameObject  Create(string id,string name,bool isSelected)
    {
        GameObject coloneItem =  GameObject.Instantiate<GameObject>(item.gameObject);
        coloneItem.transform.SetParent(item.transform.parent);
        coloneItem.transform.localScale = Vector3.one;
        coloneItem.transform.rotation = Quaternion.identity;
        coloneItem.name = id.ToString();
        coloneItem.SetActive(true);
        coloneItem.transform.Find("name").GetComponent<Text>().text = name;
        coloneItem.GetComponent<Toggle>().isOn = isSelected;

        return coloneItem;
 


    }
    public void  SetGame(GameObject coloneItem, int grounpId ,int runState, bool isDefence,int index)
    {
        if (coloneItem.transform.Find("GrounpState") != null)
        {
            if (runState == 0)
            {
                coloneItem.transform.Find("GrounpState").GetComponent<GrounpStateButton>().SwitchState();
            }

            coloneItem.transform.Find("GrounpState").GetComponent<GrounpStateButton>().grounpId = grounpId.ToString();
            coloneItem.transform.Find("GrounpState").GetComponent<GrounpStateButton>().grounpName = name;

            //玩家或者非当前的管理员所属的游戏
            if(LoginInfo.Userinfo.type == 0 || (LoginInfo.Userinfo.type == 1 && index!=0))
            {
                coloneItem.transform.Find("GrounpState").GetComponent<Button>().enabled = false;

            }
            else
            {
                coloneItem.transform.Find("GrounpState").GetComponent<Button>().enabled = true;
            }
        }
        if (coloneItem.transform.Find("SetFence") != null)
        {
            if (isDefence)
            {
                coloneItem.transform.Find("SetFence").GetComponent<FenceStateButton>().Change();
            }
        }
    }

   public void SetGrountp(GameObject coloneItem, int userCount,bool runState,bool isCurrentUser)
    {
        if (coloneItem.transform.Find("count") != null)
        {
            coloneItem.transform.Find("count").GetComponentInChildren<Text>().text = userCount.ToString();
            if (isCurrentUser)
            {
                coloneItem.transform.Find("count").GetComponent<Image>().color = new Color32(204, 51, 255, 255);
            }
        }
    }

    public void SetUser(bool runState)
    {

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
    /// 选择队
    /// </summary>
    /// <param name="toggle"></param>
    public void GrounpToggleChange(Toggle toggle)
    {
        if(toggle.isOn)
        {
            string gameId = toggle.name;
            GetComponentInParent<ListView>().selectGameId = gameId;
            string itemName = toggle.transform.Find("name").GetComponent<Text>().text;
            if(GetComponentInParent<RootEditGameView>()!=null)
            {
                GetComponentInParent<RootEditGameView>().gameEditView.ShowGameData(int.Parse( gameId));
               
            }
            GetComponentInParent<RootBaseRoomView>().CallSearchSingleRoomAction(gameId);

        }
       
    }

    /// <summary>
    /// 选择房间
    /// </summary>
    /// <param name="toggle"></param>
    public void RoomToggleChange(Toggle toggle)
    {
        if (toggle.isOn)
        {
            string roomId = toggle.name;
            GetComponentInParent<ListView>().selectRoomId = roomId;
            string itemName = toggle.transform.Find("name").GetComponent<Text>().text;
            if(GetComponentInParent<RootEditGameView>()!=null)
            {
                //GetComponentInParent<RootEditGameView>().gameEditView.ShowGrounpName(itemName);

                //Room room = GetComponentInParent<RootEditGameView>().roomListView.FindRoomByKey(grounpId);
                //if (room != null)
                //{
                //    GetComponentInParent<RootEditGameView>().gameEditView.ShowCheckCode(room.checkCode);
                //}
            }
            GetComponentInParent<RootBaseRoomView>().CallSearchSingleGrounpAction(roomId);
        }
    }
}
