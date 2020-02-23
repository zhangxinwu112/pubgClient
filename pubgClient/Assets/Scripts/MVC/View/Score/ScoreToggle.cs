using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreToggle : MonoBehaviour {

    public void TooglePlayer(Toggle toggle)
    {
        if(toggle.isOn)
        {
            GetComponentInParent<ScoreView>().RequstData(int.Parse( toggle.name));
        }
    }


    public void ToogleCurrent(Toggle toggle)
    {

    }
}
