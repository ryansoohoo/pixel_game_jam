using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public Animator anim;
    public SpriteRenderer spriteChild;
    public float dashSpeed = 15.0f;          // Speed during a dash
    public float swimKickSpeed = 3f;          // Speed during a dash
    public float dashDuration = 0.05f;        // How long the dash lasts
    public float acceleration = 1.0f;        // Acceleration rate
    public float deceleration = 0.95f;       // Deceleration rate when no input is provided
    public Vector2 currentForce = new Vector2(0.1f, 0); // Simulated water current
    public float buoyancy = 0.5f;            // Buoyancy effect strength
    public float buoyancyFrequency = 2f;     // Frequency of buoyancy oscillation

    private Vector2 currentVelocity;
    private float dashTimeLeft = 0f;         // Timer to manage dash duration
    private bool isDashing = false;          // Is the player currently dashing?
    private Vector2 inputRaw;


    void Update()
    {
        anim.SetFloat("x", inputRaw.x);
        anim.SetFloat("y", inputRaw.y);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartDash();
        }
        spriteChild.flipX = inputRaw.x <     0;
        float angle = Mathf.Atan2(inputRaw.y, inputRaw.x) * Mathf.Rad2Deg;
        //spriteChild.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
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

    public void _AnimEventSwimKick()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        currentVelocity = inputRaw.normalized * swimKickSpeed;
        rb.velocity = currentVelocity;
    }
}
