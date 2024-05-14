using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public Animator anim;
    public SpriteRenderer spriteChild;
    public float dashSpeed = 15.0f;
    public float swimKickSpeed = 3f;
    public float dashDuration = 0.05f;
    public float acceleration = 1.0f;
    public float deceleration = 0.95f;
    public Vector2 currentForce = new Vector2(0.1f, 0);
    public float buoyancy = 0.5f;
    public float buoyancyFrequency = 2f;
    public float divingDuration = 1.0f;
    public float waterSurfaceY = 0.0f;
    public float waterThreshold = 0.1f;

    private Vector2 inputRaw;
    private Vector2 currentVelocity;
    private float dashTimeLeft = 0f;
    private bool isDashing = false;
    private bool isInWater = true;
    private float divingTimeLeft = 0f;
    float divespeed = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        inputRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (isInWater)
        {
            spriteChild.flipX = inputRaw.x < 0;
            anim.SetFloat("x", inputRaw.x);
            anim.SetFloat("y", inputRaw.y);
        }
        else
        {
            spriteChild.flipX = rb.velocity.x < 0;
            anim.SetFloat("x", rb.velocity.x);
            anim.SetFloat("y", rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && isInWater && divingTimeLeft <= 0)
        {
            StartDash();
        }
        //float angle = Mathf.Atan2(inputRaw.y, inputRaw.x) * Mathf.Rad2Deg;
        //spriteChild.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

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
            rb.velocity = new Vector2(rb.velocity.x, -divespeed);
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
            if (inputRaw.magnitude > 0)
            {
                currentVelocity += inputRaw.normalized * acceleration * Time.fixedDeltaTime;
                currentVelocity = Vector2.ClampMagnitude(currentVelocity, speed);
            }
            else
            {
                currentVelocity *= deceleration;
            }

            rb.velocity = currentVelocity;
            ApplyBuoyancyEffect();
        }
        else
        {
            rb.gravityScale = 1.0f;
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
        rb.gravityScale = 1;
    }

    float velocityenter;
    public override void WaterEnter()
    {
        isInWater = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(rb.velocity.x, -speed * 2);
        divingTimeLeft = divingDuration;
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
