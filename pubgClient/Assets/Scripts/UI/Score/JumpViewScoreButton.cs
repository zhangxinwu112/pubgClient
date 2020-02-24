using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpViewScoreButton : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponentInChildren<Button>().onClick.AddListener(() => {
            SceneTools.instance.LoadScene("ScoreOrder");

        });
    }
	
}
