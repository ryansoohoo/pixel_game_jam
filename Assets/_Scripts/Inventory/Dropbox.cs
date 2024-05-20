using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System.CodeDom;

public class Dropbox : Triggerbox
{
    //audio
    [SerializeField] private EventReference dropOffSound;
    public Otterboat otterboat;
    public override void OnEnter()
    {
        Player.instance.inDropBox = true;
        if (Player.instance.inventory.HasItem())
        {
            MainManager.instance.MoveItem(Player.instance.inventory.collectables[0],Player.instance.inventory,otterboat.inventory);
            AudioManager.instance.PlayOneShot(dropOffSound, this.transform.position);
        }
        CameraFollow.instance.inBoat = true;
    }

    public override void OnExit()
    {
        CameraFollow.instance.inBoat = false;
        Player.instance.inDropBox = false;
    }
}
