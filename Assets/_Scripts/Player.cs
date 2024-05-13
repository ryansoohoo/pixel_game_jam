using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public float dashSpeed = 15.0f;          // Speed during a dash
    public float dashDuration = 0.2f;        // How long the dash lasts
    public float acceleration = 1.0f;
    public float deceleration = 0.95f;
    public float dragCoefficient = 0.1f;
    public Vector2 currentForce = new Vector2(0.1f, 0); // for currents
    public float buoyancy = 0.5f;
    public float buoyancyFrequency = 2f;

    private Vector2 currentVelocity;
    private float dashTimeLeft = 0f;         // Timer to manage dash duration
    private bool isDashing = false;          // Is the player currently dashing?

    Vector2 inputRaw;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        // Check for dash input (using Shift key as an example)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            dashTimeLeft -= Time.fixedDeltaTime;
            if (dashTimeLeft <= 0)
            {
                EndDash();
            }
        }
        else
        {
            inputRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            currentVelocity += currentForce * Time.fixedDeltaTime;

            if (inputRaw.magnitude > 0) // Apply acceleration
            {
                currentVelocity += inputRaw.normalized * acceleration * Time.fixedDeltaTime;
                currentVelocity = Vector2.ClampMagnitude(currentVelocity, speed);
            }
            else // Apply deceleration
            {
                currentVelocity *= deceleration;
                ApplyFluidDrag();
            }

            rb.velocity = currentVelocity;
            ApplyBuoyancyEffect();
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        currentVelocity = inputRaw.normalized * dashSpeed; // Set the current velocity to dash speed in the current direction
        rb.velocity = currentVelocity;
    }

    void EndDash()
    {
        isDashing = false;
    }

    void ApplyBuoyancyEffect()
    {
        float buoyancyOffset = Mathf.Sin(Time.fixedTime * Mathf.PI * buoyancyFrequency) * buoyancy;
        if (!isDashing) // Avoid applying buoyancy while dashing
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + buoyancyOffset);
        }
    }

    void ApplyFluidDrag()
    {
        if (!isDashing) // Avoid drag during a dash
        {
            float speed = currentVelocity.magnitude;
            Vector2 drag = dragCoefficient * speed * speed * -currentVelocity.normalized;
            currentVelocity += drag * Time.fixedDeltaTime;
        }
    }
}
