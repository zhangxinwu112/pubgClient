using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {

    // Use this for initialization
    public ToggleGroup upToggleGrounp;

    public ToggleGroup leftGrounp;

    //玩家排行
    public Button playerOrderButton;

    //玩家排行
    public Button GrounpOrderButton;



    public GameObject item;
    void Start () {
        RequstData(0);

    }
	
	
    public void RequstData(int currentPlayer)
    {
        RestFulProxy.SearchScore(currentPlayer, (result) => {

            result = result.Trim('"');
            result = result.Replace("\\", "");
            List<Score> list = Utils.CollectionsConvert.ToObject<List<Score>>(result);
            Create(list);
        });
    }
    List<GameObject> createLsit = new List<GameObject>();
    private void Create(List<Score> list)
    {
        DeleteCreateList();
        for (int  i =0;i< list.Count;i++)
        {
            GameObject newObject = TransformControlUtility.CreateItem(item, item.transform.parent);
            newObject.SetActive(true);
            newObject.transform.Find("index").GetComponent<Text>().text = (i + 1).ToString();
            newObject.transform.Find("name").GetComponent<Text>().text = list[i].userName;
            newObject.transform.Find("score").GetComponent<Text>().text = list[i].fightScore.ToString();
            newObject.transform.Find("life").GetComponent<Text>().text = list[i].lifeValue.ToString();
            createLsit.Add(newObject);
        }
       
    }


    private void DeleteCreateList()
    {
        createLsit.ForEach((g) => {
            GameObject.Destroy(g);

        });
        createLsit.Clear();
    }
}
