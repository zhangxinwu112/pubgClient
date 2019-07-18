using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScene : MonoBehaviour {

	
    public void Jump(string sceneName)
    {
        SceneTools.instance.LoadScene(sceneName,null);
    }
}
