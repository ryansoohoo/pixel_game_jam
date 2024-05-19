using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public static Player instance;
    public Inventory inventory;
    public Animator anim;
    public SpriteRenderer spriteChild;
    public float dashSpeed = 15.0f;
    public float swimKickSpeed = 3f;
    public float dashDuration = 0.05f;
    public float acceleration = 1.0f;
    public float deceleration = 0.95f;
    public Vector2 currentForce = new Vector2(0.1f, 0);
    public float divingDuration = 1.0f;
    public float waterSurfaceY = 0.0f;
    public float waterThreshold = 0.1f;
    public bool inDropBox = false;

    private Vector2 inputRaw;
    private Vector2 currentVelocity;
    private float dashTimeLeft = 0f;
    private bool isDashing = false;
    private bool isInWater = true;
    private float divingTimeLeft = 0f;
    float divespeed = 1f;

    
    private void Awake()
    {
        if(instance == null) instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        float yPos = transform.position.y;
        if (yPos > waterSurfaceY + waterThreshold && isInWater && divingTimeLeft <= 0)
        {
            WaterExit();
        }
        else if (yPos < waterSurfaceY - waterThreshold && !isInWater)
        {
            WaterEnter();
        }

        inputRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (isInWater) {
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && isInWater && divingTimeLeft <= 0) {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (divingTimeLeft > 0) {
            divingTimeLeft -= Time.fixedDeltaTime;
            rb.velocity = new Vector2(rb.velocity.x, -divespeed);
            currentVelocity = Vector3.zero;
            return;
        }
        
        
        if (isDashing) {
            dashTimeLeft -= Time.fixedDeltaTime;
            if (dashTimeLeft <= 0) {
                EndDash();
                return;
            }
            return;
        }
        
        if (isInWater) {
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
            if(!isDashing)
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

    public override void WaterExit()
    {
        isInWater = false;
        rb.gravityScale = 1;
    }

    public override void WaterEnter()
    {
        isInWater = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(rb.velocity.x, -speed * 2);
        divingTimeLeft = divingDuration;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Triggerbox trigger = collision.gameObject.GetComponent<Triggerbox>();
        if (trigger == null)
            return;

        trigger.OnEnter();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Triggerbox trigger = collision.gameObject.GetComponent<Triggerbox>();
        if (trigger == null)
            return;

        trigger.OnExit();
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
