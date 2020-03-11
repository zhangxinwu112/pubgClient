using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddLife : MonoBehaviour {

    [SerializeField]
    private InputField playerName;

    [SerializeField]
    private InputField playerCurrentLifeName;


    [SerializeField]
    private InputField playerBulletCountInput;


    //////[SerializeField]
    //////private InputField AddPlayerLifeName;

    [SerializeField]
    private Button okButton;

    [SerializeField]
    private Button cancelButton;

    [SerializeField]
    private Button closeButton;

    public static AddLife instance;
    private void Awake()
    {
        instance = this;
    }

    void Start () {

        ShowOrHide(false);
        cancelButton.onClick.AddListener(()=> {
            ShowOrHide(false);
        });

        closeButton.onClick.AddListener(() => {
            ShowOrHide(false);
        });

        okButton.onClick.AddListener(AddLifeClick);



    }
	
	public void AddLifeClick()
    {
        
        string lifeValue = playerCurrentLifeName.text.Trim();
        if (string.IsNullOrEmpty(lifeValue))
        {
            GetComponentInParent<RootEditGameView>().errorMessage.ShowMessage("设定的命值不能为空！", SoundType.Error);

            return;
        }

        string bulletCountValue = playerBulletCountInput.text.Trim();

        if (string.IsNullOrEmpty(bulletCountValue))
        {
            GetComponentInParent<RootEditGameView>().errorMessage.ShowMessage("设定的弹量不能为空！", SoundType.Error);

            return;
        }



        
        RestFulProxy.AddLife(lifeValue, bulletCountValue, userId.ToString(), (result) => {

            });

            GetComponentInParent<RootEditGameView>().errorMessage.ShowMessage("操作成功",SoundType.Success);
            ShowOrHide(false);
        
    }

    public void ShowOrHide(bool isShow)
    {
        if(isShow)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = Vector3.zero;
        }
    }
    private int userId = -1;
    public void ShowFormData(Life life)
    {
        userId = life.userId;
        playerName.text = life.userName;
        playerCurrentLifeName.text = life.lifeValue.ToString();
        playerBulletCountInput.text = life.bulletCount.ToString();
    }

    

}
