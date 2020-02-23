using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchEnterState : MonoBehaviour {

    [SerializeField]
    private Button enterGameButton;
	// Use this for initialization
	void Start () {
        GetComponent<Button>().interactable = false;
        if(enterGameButton!=null)
        {
            enterGameButton.interactable = true;
        }
        
        StartCoroutine(CheckEnterState());
    }
	
	

    private IEnumerator CheckEnterState()
    {
        while (true)
        {

            RestFulProxy.CheckEnterButton((result) => {

                result = result.Trim('"');
                if(result.Equals("0"))
                {
                    GetComponent<Button>().interactable = true;
                    SoundUtilty.PlayResouceSound("Sound/GameStart");
                    if(enterGameButton!=null)
                    {
                        enterGameButton.interactable = false;
                    }
                   

                    StopAllCoroutines();
                }

            });
            yield return new WaitForSeconds(4.0f);

        }
    }
}
