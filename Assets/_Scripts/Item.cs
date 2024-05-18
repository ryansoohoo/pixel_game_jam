using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Collectable collectable;

    public Collectable PickUp()
    {
        Destroy(gameObject);
        return collectable;
    }
}
