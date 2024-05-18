using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropbox : Triggerbox
{
    public override void OnEnter()
    {
        Player.instance.inDropBox = true;
    }

    public override void OnExit()
    {
        Player.instance.inDropBox = false;
    }
}
