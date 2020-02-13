using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///用来遮罩的透明画布
/// </summary>
public class TransparentMaskManager : MonoSingleton<TransparentMaskManager> {

    private GameObject mask;

    public void Show(bool isHideButton = true)
    {
        
        mask = TransformControlUtility.CreateItem("Mask", UIUtility.GetRootCanvas());
        mask.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        if(isHideButton)
        {
            mask.GetComponentInChildren<Button>().gameObject.SetActive(false);
        }
    }

    public void SetTransparent(byte transparentValue)
    {
        mask.GetComponent<Image>().color = new Color32(255, 255, 255, transparentValue);
    }
 
    public void Hide()
    {
        if(mask!=null)
        {
            GameObject.Destroy(mask);
        }
    }
}
