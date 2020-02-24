using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TestStr : MonoBehaviour {

	// Use this for initialization
	void Start () {

        string aa = "ffg\r\n";
        string[] strs = Regex.Split(aa, "\u0020");
        Debug.Log(strs[0]);

        


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
