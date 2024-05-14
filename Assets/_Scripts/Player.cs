using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public Animator anim;
    public SpriteRenderer spriteChild;
    public float dashSpeed = 15.0f;          // Speed during a dash
    public float swimKickSpeed = 3f;         // Speed during a swim kick
    public float dashDuration = 0.05f;       // How long the dash lasts
    public float acceleration = 1.0f;        // Acceleration rate
    public float deceleration = 0.95f;       // Deceleration rate when no input is provided
    public Vector2 currentForce = new Vector2(0.1f, 0); // Simulated water current
    public float buoyancy = 0.5f;            // Buoyancy effect strength
    public float buoyancyFrequency = 2f;     // Frequency of buoyancy oscillation
    public float divingDuration = 1.0f;      // Duration of the diving state
    public float waterSurfaceY = 0.0f;       // Y-coordinate of the water surface
    public float waterThreshold = 0.1f;      // Threshold for transitioning at the water surface
    
    private Vector2 inputRaw;
    private Vector2 currentVelocity;
    private float dashTimeLeft = 0f;
    private bool isDashing = false;
    private bool isInWater = true;
    private float divingTimeLeft = 0f;       // Timer for how long the diving state lasts
    float divespeed = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        inputRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        anim.SetFloat("x", inputRaw.x);
        anim.SetFloat("y", inputRaw.y);
        if (Input.GetKeyDown(KeyCode.LeftShift) && isInWater && divingTimeLeft <= 0)
        {
            StartDash();
        }
        spriteChild.flipX = inputRaw.x < 0;
        float angle = Mathf.Atan2(inputRaw.y, inputRaw.x) * Mathf.Rad2Deg;
        spriteChild.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        // Transition check improved to manage diving state correctly
        float yPos = transform.position.y;
        if (yPos > waterSurfaceY + waterThreshold && isInWater && divingTimeLeft <= 0)
        {
            WaterExit();
        }
        else if (yPos < waterSurfaceY - waterThreshold && !isInWater)
        {
            WaterEnter();
        }
    }

    void FixedUpdate()
    {
        if (divingTimeLeft > 0)
        {
            divingTimeLeft -= Time.fixedDeltaTime;
           
            rb.velocity = new Vector2(rb.velocity.x, -divespeed) ;  // Ensure a constant downward movement
            currentVelocity = Vector3.zero;

            velocityenter = 0;
        }
        else if (isDashing)
        {
            dashTimeLeft -= Time.fixedDeltaTime;
            if (dashTimeLeft <= 0)
            {
                EndDash();
            }
        }
        else if (isInWater)
        {
            if (inputRaw.magnitude > 0) // acceleration
            {
                currentVelocity += inputRaw.normalized * acceleration * Time.fixedDeltaTime;
                currentVelocity = Vector2.ClampMagnitude(currentVelocity, speed);
            }
            else // deceleration
            {
                currentVelocity *= deceleration;
            }

            rb.velocity = currentVelocity;
            ApplyBuoyancyEffect();
        }
        else
        {
            rb.gravityScale = 1.0f; // Apply gravity when out of water
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        currentVelocity = inputRaw.normalized * dashSpeed;
        rb.velocity = currentVelocity;
    }

    void EndDash()
    {
        isDashing = false;
    }

    void ApplyBuoyancyEffect()
    {
        float buoyancyOffset = Mathf.Sin(Time.fixedTime * Mathf.PI * buoyancyFrequency) * buoyancy;
        if (!isDashing)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + buoyancyOffset);
        }
    }

    public override void WaterExit()
    {
        velocityenter = rb.velocity.magnitude;
        isInWater = false;
        rb.gravityScale = 1; // Apply gravity when the player is out of water
    }

    float velocityenter;
    public override void WaterEnter()
    {

        isInWater = true;
        rb.gravityScale = 0; // Remove gravity effect in water
        rb.velocity = new Vector2(rb.velocity.x, -speed * 2); // Give a strong initial downward boost
        divingTimeLeft = divingDuration; // Start the diving state duration
    }

    public void _AnimEventSwimKick()
    {
        if (!isInWater)
            return;
        isDashing = true;
        dashTimeLeft = dashDuration;
        currentVelocity = inputRaw.normalized * swimKickSpeed;
        rb.velocity = currentVelocity;
    }
}
