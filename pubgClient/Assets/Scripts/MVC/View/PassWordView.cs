using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassWordView : MonoBehaviour
{

    private InputField passWord;


    private void Start()
    {
        passWord = GetComponent<InputField>();
    }

    public string  GetInputFieldText()
    {
        return passWord.text;
    }

}
