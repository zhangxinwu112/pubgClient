using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineCodeProxy : Proxy
{
    public new const string NAME = "MachineCodeProxy";

    public MachineCodeProxy() : base(NAME)
    {

    }

    public void CodeSubmit(string activeCode)
    {
        string serverPath = Config.parse("ServerPath");
    }

   
}
