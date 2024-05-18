using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player
    public Rigidbody2D rb; // Reference to the Rigidbody2D component

    public float buoyancy = 0.5f;
    public float buoyancyFrequency = 2f;
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

    public void ApplyBuoyancyEffect()
    {
        float buoyancyOffset = Mathf.Sin(Time.fixedTime * Mathf.PI * buoyancyFrequency) * buoyancy;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + buoyancyOffset);

    }
}
