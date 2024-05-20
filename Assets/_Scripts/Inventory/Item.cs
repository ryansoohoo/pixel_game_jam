using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Unit
{

    public Collectable collectable;

    public void Start()
    {
        collectable = SpawnPattern.instance.GetRandomCollectable();
        GetComponent<SpriteRenderer>().sprite = collectable.image;
        rb = GetComponent<Rigidbody2D>();
        buoyancy = Random.Range(0.25f, 0.5f);
        buoyancyFrequency = Random.Range(1f, 2f);
    }

    public Collectable PickUp()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.hatCollected, this.transform.position);
        Destroy(gameObject);
        return collectable;
    }

    public void FixedUpdate()
    {
        //ApplyBuoyancyEffect();
    }
}
