using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public void Awake()
    {
        instance = this; 
    }

    public void MoveItem(Collectable item, Inventory inv1, Inventory inv2)
    {
        // animation or something to move item from player inventory to boat inventory
        inv1.collectables.Remove(item);
        inv2.collectables.Add(item);
    }
}
