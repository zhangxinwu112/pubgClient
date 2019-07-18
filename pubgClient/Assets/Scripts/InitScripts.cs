using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScripts : MonoBehaviour {

    public void Initialized()
    {
       gameObject.AddComponent<SceneTools>();
       gameObject.AddComponent<SockeService>();
       gameObject.AddComponent<NetWorkCheck>();
    }
}
