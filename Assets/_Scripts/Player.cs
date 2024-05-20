using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class Player : Unit
{
    public static Player instance;
    public Inventory inventory;
    public Animator anim;
    public SpriteRenderer spriteChild;
    public SpriteRenderer hat;
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

<<<<<<< HEAD
    private EventInstance otterSwim;
    
=======
    protected Animator animator;
    protected AnimatorOverrideController animatorOverrideController;
>>>>>>> e619b118715fbb3dbfb5cfa9cd2aa46242c4ee8a
    private void Awake()
    {
        if(instance == null) instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
<<<<<<< HEAD
        otterSwim = AudioManager.instance.CreateInstance(FMODEvents.instance.otterSwim);
=======

        Animator hatAnim = hat.GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(hatAnim.runtimeAnimatorController);
        hatAnim.runtimeAnimatorController = animatorOverrideController;
>>>>>>> e619b118715fbb3dbfb5cfa9cd2aa46242c4ee8a
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
            //spriteChild.flipX = inputRaw.x < 0;
            //hat.flipX = inputRaw.x < 0;
            anim.SetFloat("x", inputRaw.x);
            anim.SetFloat("y", inputRaw.y);
        }
        else
        {
            //spriteChild.flipX = rb.velocity.x < 0;
            //hat.flipX = rb.velocity.x < 0;
            anim.SetFloat("x", rb.velocity.x);
            anim.SetFloat("y", rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isInWater && divingTimeLeft <= 0) {
            StartDash();
        }
    }

    private void UpdateSound()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.8 || Mathf.Abs(rb.velocity.y) > 0.8)
        {
            PLAYBACK_STATE playbackState;
            otterSwim.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                otterSwim.start();
            }
        }

        else
        {
            otterSwim.stop(STOP_MODE.ALLOWFADEOUT);
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

        UpdateSound();
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

    public void Equip(Collectable collectable)
    {
        if(collectable.idle != null)
        {
            hat.GetComponent<Animator>().enabled = true;
            animatorOverrideController["idle"] = collectable.idle;
        }
        else
        {
            hat.sprite = collectable.image;
            hat.GetComponent<Animator>().enabled = false;
        }
    }

    public void RemoveHat(Collectable collectable)
    {
        hat.GetComponent<Animator>().enabled = false;

        hat.sprite = null;
        print("remove hat");
    }
}
