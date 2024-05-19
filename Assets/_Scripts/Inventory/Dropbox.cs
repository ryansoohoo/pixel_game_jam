using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropbox : Triggerbox
{
    public Otterboat otterboat;
    public override void OnEnter()
    {
        Player.instance.inDropBox = true;
        if (Player.instance.inventory.HasItem())
        {
            MainManager.instance.MoveItem(Player.instance.inventory.collectables[0],Player.instance.inventory,otterboat.inventory);    
        }
    }

    public override void OnExit()
    {
        Player.instance.inDropBox = false;
    }
}
