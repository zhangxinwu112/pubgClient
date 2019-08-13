using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizationHeight : MonoBehaviour {
    [SerializeField]
    public Transform item;

    private float sizeDataHight = 0.0f;

    private float defaultWidth = 0.0f;

    private void Start()
    {
        defaultWidth = GetComponent<RectTransform>().sizeDelta.x;
    }
    void Update () {
        sizeDataHight = 0.0f;
        foreach(Transform child in item)
        {
            sizeDataHight = sizeDataHight + child.GetComponent<RectTransform>().sizeDelta.y;
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(defaultWidth, sizeDataHight);
            
    }
}
