using DataModel;
using server.Model;
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
                coloneItem.transform.Find("GrounpState").GetComponent<Button>().interactable = false;

            }
            else
            {
                coloneItem.transform.Find("GrounpState").GetComponent<Button>().interactable = true;
            }
        }
        if (coloneItem.transform.Find("SetFence") != null)
        {
            if (isDefence)
            {
                coloneItem.transform.Find("SetFence").GetComponent<ChangeStateButton>().Change();
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
            //只读
            coloneItem.transform.Find("count").GetComponent<Button>().interactable = false;
        }
        //只读
        if (coloneItem.transform.Find("ok") != null)
        {
            if(runState)
            {
                coloneItem.GetComponentInChildren<ChangeStateButton>().Change();
            }
           
            coloneItem.transform.Find("ok").GetComponent<Button>().interactable = false;
        }

    }

    public void SetUser(GameObject newObject, UserItem userItem)
    {
        //已经准备好
        if (userItem.runState == 0)
        {
            newObject.GetComponentInChildren<ChangeStateButton>().Change();
        }
            //管理员
        if (LoginInfo.Userinfo.type == 1)
        {
            newObject.GetComponentInChildren<ChangeStateButton>().GetComponent<Button>().interactable = false;
        }
        else
        {
            if (userItem.runState == 0)
            {
                //运行起来后
                newObject.GetComponentInChildren<ChangeStateButton>().GetComponent<Button>().interactable = false;
            }
            else
            {
                if(LoginInfo.Userinfo.id != userItem.id)
                {
                    newObject.GetComponentInChildren<ChangeStateButton>().GetComponent<Button>().interactable = false;
                }
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
            GetComponentInParent<RootBaseRoomView>().CallSearchSingleGrounpAction(roomId);
        }
    }

    public void Ready(Toggle toggle)
    {
        //if(!toggle.name.Equals(LoginInfo.Userinfo.id.ToString()))
        //{
        //    GetComponentInParent<RootBaseRoomView>().errorMessage.ShowMessage("非法操作");
        //    return;
        //}
       // NGUIDebug.Log(toggle.name);
        RestFulProxy.SetUserState(LoginInfo.Userinfo.id, (result) => {

            //NGUIDebug.Log(result);
            result = result.Trim('"');
            if (result.Equals("0"))
            {
                GetComponentInParent<RootBaseRoomView>().errorMessage.ShowMessage("操作成功");
            }
            else if (result.Equals("-2"))
            {
                GetComponentInParent<RootBaseRoomView>().errorMessage.ShowMessage("操作错误，该对不能只有一个玩家");
            }else
            {
                GetComponentInParent<RootBaseRoomView>().errorMessage.ShowMessage("操作错误，其他玩家未准备就绪");
            }

        }, null);
       // Debug.Log(toggle.name);
    }
}
