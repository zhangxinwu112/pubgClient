using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkButton : Button
{
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
       // base.DoStateTransition(state, instant);
        switch (state)
        {

            case SelectionState.Disabled:

                break;

            case SelectionState.Highlighted:

                CreateLinkButton();
                break;

            case SelectionState.Normal:

                DestroyText();
                break;

            case SelectionState.Pressed:

                //Debug.Log("Pressed！");
                break;

            default:
                
                break;

        }

    }

    private Text underline = null;


    public void CreateLinkButton()
    {
        Text text = GetComponentInChildren<Text>();
        if (text == null)
        {
            return;
        }
        if(underline!=null)
        {
            return;
        }
        underline = Instantiate(text) as Text;
        underline.name = "Underline";
        underline.transform.SetParent(text.transform);
        RectTransform rt = underline.rectTransform;
        //设置下划线坐标和位置  
        rt.anchoredPosition3D = Vector3.zero;
        rt.offsetMax = Vector2.zero;
        rt.offsetMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.anchorMin = Vector2.zero;
        underline.text = "_";
        float perlineWidth = underline.preferredWidth;      //单个下划线宽度  
        //Debug.Log(perlineWidth);
        float width = text.preferredWidth;
        int lineCount = (int)Mathf.Round(width / perlineWidth);
        underline.text = "";
        for (int i = 1; i < lineCount; i++)
        {
            underline.text += "_";
        }

        if (text.GetComponent<Text>().fontSize>45)
        {
            underline.GetComponent<Text>().fontSize = text.GetComponent<Text>().fontSize - 15;
        }
    }

    private void DestroyText()
    {
        if (underline != null)
        {
            GameObject.Destroy(underline);
        }
    }


}
