using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : Enemy
{
    public float speed;
    BoxCollider2D wallCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        enemyCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        wallCollider = gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (isAlive == true)
        {
            rb.velocity = new Vector2(1f * speed, rb.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy Hitbox")
        {
            speed = -speed;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public override void Die()
    {
        animator.SetBool("isDead", true);
        isAlive = false;
        rb.velocity = new Vector2(0f, 0f);
        rb.simulated = false;
        enemyCollider.enabled = false;
        wallCollider.enabled = false;
        StartCoroutine(WaitUntilSetInactive());
        StopCoroutine(WaitUntilSetInactive());
    }
}
