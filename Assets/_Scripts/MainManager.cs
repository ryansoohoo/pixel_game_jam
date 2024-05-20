using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public Animator oscar;
    public Animator olga;
    public Animator octavia;
    public Animator omar;

    public void Awake()
    {
        instance = this; 
    }

    public void MoveItem(Collectable item, Inventory inv1, Inventory inv2)
    {
        // animation or something to move item from player inventory to boat inventory
        inv1.RemoveCollectable(item);
        inv2.AddCollectable(item);

        UIManager.Instance.StartTyping(item.name,item.desc);
        oscar.SetTrigger("0");
        olga.SetTrigger("0");
        octavia.SetTrigger("0");
        omar.SetTrigger("0");
    }
}
