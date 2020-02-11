using server.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    private Text userText;


    [SerializeField]
    private Button quitButton;


    [SerializeField]
    private Button backButton;

    public static MainView instacne;
    private void Awake()
    {
        instacne = this;
    }
    void Start () {
        quitButton.onClick.AddListener(()=> {

            Application.Quit();
        });

        backButton.onClick.AddListener(() => {

            string currentScene = PlayerPrefs.GetString("currrentScene");
           
            if(string.Compare(currentScene, "Game") ==0)
            {
                SceneTools.instance.BackScene();
            }
            else
            {
                SceneTools.instance.LoadScene("Login");
                LoingStateReset();
            }
            

        });

        userText.text = "";

        backButton.gameObject.SetActive(false);

    }

    private void LoingStateReset()
    {
        LoginInfo.Userinfo = null;
        backButton.gameObject.SetActive(false);
        userText.text = "";
        SocketService.instance.CloseSocket();
    }

    public void SetUserInfo(UserItem _user)
    {
        if(_user.type ==0)
        {
            userText.text = "欢迎您："+ _user.name;
        }else if(_user.type == 1)
        {
            userText.text = "欢迎您管理员：" + _user.name;
        }else
        {
            userText.text = "欢迎您道具：" + _user.name;
        }
        
    }

   

    public void ShowHideBack(bool isShow)
    {
        backButton.gameObject.SetActive(isShow);
    }

}
