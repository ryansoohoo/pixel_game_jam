using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Collectable> collectables = new List<Collectable>();
    public int maxSize = 1;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        Item item = gameObject?.GetComponent<Item>();
        if(collectables.Count < maxSize)
            AddCollectable(item.PickUp());
    }

    public void AddCollectable(Collectable collectable)
    {
        if (collectable != null && !collectables.Contains(collectable))
        {
            collectables.Add(collectable);
            Debug.Log("Collectable added: " + collectable.name);
        }
        else
        {
            Debug.LogWarning("Collectable is either null or already in the inventory.");
        }
    }

    public void RemoveCollectable(Collectable collectable)
    {
        if (collectable != null && collectables.Contains(collectable))
        {
            collectables.Remove(collectable);
            Debug.Log("Collectable removed: " + collectable.name);
        }
        else
        {
            Debug.LogWarning("Collectable is either null or not in the inventory.");
        }
    }
}
