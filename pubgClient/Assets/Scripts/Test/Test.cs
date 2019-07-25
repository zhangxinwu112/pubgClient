using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(Delay());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(5.0f);
        string functionName = "addMarker1" + "()";
        GetComponent<UniWebView>().EvaluateJavaScript(functionName);
    }
}
