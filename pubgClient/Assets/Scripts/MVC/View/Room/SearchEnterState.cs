using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchEnterState : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().interactable = false;
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
                    StopAllCoroutines();
                }

            });
            yield return new WaitForSeconds(4.0f);

        }
    }
}
