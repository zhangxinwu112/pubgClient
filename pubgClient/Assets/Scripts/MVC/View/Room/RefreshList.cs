﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshList : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponent<Button>().onClick.AddListener(() => {

            EventMgr.Instance.SendEvent(EventName.UPDATE_PLAYER_STATE,null);
        });
	}
	
}
