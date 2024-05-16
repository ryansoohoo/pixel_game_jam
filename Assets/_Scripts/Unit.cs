using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player

    public Rigidbody2D rb; // Reference to the Rigidbody2D component

    public void Move(Vector2 input)
    {
        // Move the player
        rb.velocity = new Vector2(input.x, input.y) * speed;
    }

    public virtual void WaterExit()
    {

    }

    public virtual void WaterEnter()
    {

    }
}
