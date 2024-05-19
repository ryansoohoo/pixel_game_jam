using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Unit
{
    public Collectable collectable;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        buoyancy = Random.Range(0.25f, 0.5f);
        buoyancyFrequency = Random.Range(1f, 2f);
    }

    public Collectable PickUp()
    {
        Destroy(gameObject);
        return collectable;
    }

    public void FixedUpdate()
    {
        //ApplyBuoyancyEffect();
    }
}
