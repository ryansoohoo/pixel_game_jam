using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        switch (item.characterLiking)
        {
            case (CharacterLikings.Oscar):
                oscar.SetTrigger("0");
                break;
            case (CharacterLikings.Olga):
                olga.SetTrigger("0");
                break;
            case (CharacterLikings.Octavia):
                octavia.SetTrigger("0");
                break;
            case (CharacterLikings.Omar):
                omar.SetTrigger("0");
                break;
        }
    }
}
