using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;
using UnityEngine.UI;

public class InputchangeKeySearch : MonoBehaviour {

	void Start () {

        GetComponent<InputField>().onValueChanged.AddListener((a) => {
            EventMgr.Instance.SendEvent(Constant.KEY_SEARCH, null);
        });
    }
	
}
