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
            string id = toggle.name;

            GetComponentInParent<RootRoomView>().CallSearchSingleRoomAction(id);


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
            string id = toggle.name;

            GetComponentInParent<RootRoomView>().CallSearchSingleGrounpAction(id);


        }
    }
}
