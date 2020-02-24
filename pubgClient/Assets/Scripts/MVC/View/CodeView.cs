using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class CodeView : MonoBehaviour {

    [SerializeField]
    private InputField CodeField;

    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private Text message;

    private void Start()
    {
        CodeFade.GetInstance().StartUp(gameObject);
        message.text = "";
    }

    public void OnClickCheckButton(UnityAction<Button> action)
    {
        submitButton.onClick.AddListener(()=> {

            action.Invoke(submitButton);
        });

    }

    public string GetCodeContont()
    {
        if(CodeField!=null)
        {
            return CodeField.text.Trim();
        }

        return "";
    }

    public void ShowMessage(string errrorMessage,SoundType soundType)
    {
        CodeField.text = "";
        message.text = errrorMessage;
        DOVirtual.DelayedCall(3.0f, () => {
            message.text = "";
        });
        if (soundType != SoundType.None)
        {
            SoundUtilty.PlayResouceSound("Sound/" + soundType.ToString());
        }
    }

}
