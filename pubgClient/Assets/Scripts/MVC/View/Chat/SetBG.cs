using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBG : MonoBehaviour {

    private Transform child;

    private float width;
    private float height;

    public float widthLength;
    public float heightLength;


    public float maxWidth = 0.0f;
	void Start ()
    {
        child = gameObject.transform.GetChild(0);
        maxWidth = GetComponent<RectTransform>().sizeDelta.x;
    }

    void Update ()
    {
        width = child.GetComponentInChildren<Text>().preferredWidth;
        height = child.GetComponentInChildren<Text>().preferredHeight;
        if(width + widthLength> maxWidth)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(maxWidth, height + heightLength);
        }
        else
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(width + widthLength, height + heightLength);
        }
       
    }
}
