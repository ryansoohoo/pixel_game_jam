using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{

    public List<Collectable> collectables = new List<Collectable>();
    public int maxSize = 1;
    public UnityEvent<Collectable> OnCollect;
    public bool HasItem()
    {
        return collectables.Count != 0;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        if (item == null)
            return;
        if(collectables.Count < maxSize)
            AddCollectable(item.PickUp());
    }

    public void AddCollectable(Collectable collectable)
    {
        if (collectable != null && !collectables.Contains(collectable))
        {
            OnCollect.Invoke(collectable);
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
