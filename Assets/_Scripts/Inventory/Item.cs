using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System.CodeDom;

public class Item : Unit
{
    public Collectable collectable;

    //audio
    [SerializeField] private EventReference hatCollectedSound;
        
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
        Destroy(gameObject);
        AudioManager.instance.PlayOneShot(hatCollectedSound, this.transform.position);
        return collectable;
    }

    public void FixedUpdate()
    {
        //ApplyBuoyancyEffect();
    }
}
