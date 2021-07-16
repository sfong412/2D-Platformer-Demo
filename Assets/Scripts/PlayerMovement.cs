using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5.5f;
    private float jumpHeight = 15.3f;

    private bool playerFacingRight;

    public bool isGrounded;

    bool isJumping = false;

    public float moveInput;

    public Animator animator;

    public Vector3 initialSpawnPoint, currentSpawnPoint;

    public LayerMask groundMask;
    //public LayerMask wallMask;

    Collider2D groundCheck;

    public Vector2 playerVelocity;

    Transform transform;

    IEnumerator respawnCoroutine;

    BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    BoxCollider2D feet;
    PlayerHealth health;
    SpriteRenderer sprite;
    AudioSource audio;

    public AudioClip jumpSFX;

    Level level;

    float timeFromZeroToMax = 2;

    void Awake()
    {
        GameObject feetCollider = GameObject.Find("Feet");

        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        feet = feetCollider.GetComponent<BoxCollider2D>();
        transform = GetComponent<Transform>();
        health = GetComponent<PlayerHealth>();
        sprite = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        level = GameObject.Find("Gameplay Manager").GetComponent<Level>();
        playerFacingRight = true;
        rb.simulated = true;
    }

    void Start()
    {
        initialSpawnPoint = transform.localPosition;
        currentSpawnPoint = initialSpawnPoint;
    }

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        
        //Debug.Log(isJumping);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && health.justGotDamaged == false && health.isDead == false && level.isCompleted == false)
        {
            Jump();
        }
        else
        {
           // isJumping = false;
        }

        UpdateSpeed();
        CheckPlayerDirection();
        health.Die();
        CheckIfGrounded();
        
        if (level.isCompleted == false)
        {
            moveInput = Input.GetAxis("Horizontal");

            if (health.justGotDamaged == true && health.isDead == false)
            {
                int direction = 1;

                if (playerFacingRight == true)
                {
                    direction = -direction;
                }

                rb.velocity = new Vector2(direction * 2, rb.velocity.y);
            }
            else if (health.isDead == false)
            {
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            }
            else if (health.isDead == true)
            {
            rb.velocity = new Vector2(0f, 0f); 
            }
        }
        else if (level.isCompleted == true)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            animator.SetFloat("Speed", 0);
        }
    }

    void FixedUpdate()
    {
    }

    void Jump()
    {
        if (health.isDead == false && level.isCompleted == false)
        {
           // isJumping = true;
            
            Vector2 movement = new Vector2(rb.velocity.x, jumpHeight);

            rb.velocity = movement;
        }
        audio.PlayOneShot(jumpSFX, 1f);
    }

    void CheckIfGrounded()
    {
        float extraHeight = .01f;
        float extraGroundWidth = .0f;

        Bounds playerBounds = new Bounds(new Vector3(0, 0, 0), new Vector3(feet.size.x + extraGroundWidth, feet.size.y, 0f));

        RaycastHit2D raycastHit = Physics2D.BoxCast(feet.bounds.center, feet.size, 0f, Vector2.down, extraHeight, groundMask);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded == true)
        {
            //animator.SetTrigger("Just Landed");
            //animator.Play("Land", 0, 1f);
            animator.SetBool("is Jumping", false);
        }
        else if (isGrounded == false)
        {
            animator.SetBool("is Jumping", true);
            
        }
    }

    void onLanding()
    {
    }

    void CheckPlayerDirection()
    {
        if (moveInput < 0 && health.justGotDamaged == false && health.isDead == false)
        {
            playerFacingRight = false;
        }

        if (moveInput > 0 && health.justGotDamaged == false && health.isDead == false)
        {
            playerFacingRight = true;
        }

        if (playerFacingRight == true)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
    }

    void UpdateSpeed()
    {
        if (health.isInvincible == true && health.isDead == false)
        {
            speed = 4.5f;
        }
        else
        {
            speed = 5.5f;
        }
    }

    void IsTransitioning()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector2.down) * (1 + .01f);
        Gizmos.DrawRay(transform.position, direction);
    }
}
