using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoomba : Enemy
{
    public float speed;

    private float speedAtSpawn;
    BoxCollider2D wallCollider;
    private CameraFollow camera;

    private bool goombaFacingLeft;

    public GameObject enemyLocation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        enemyCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        wallCollider = gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player").GetComponent<PlayerHealth>();

        enemy = this.gameObject;

        initialSpawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        damageValue = -1;

        speedAtSpawn = speed;
    }

    void Update()
    {
        if (player.isRespawning == false && camera.currentLevelBounds == enemyLocation)
        {
            Move();
        }
        else if (player.isRespawning == true)
        {
        }

        CheckEnemyDirection();
    }

    void Move()
    {
        if (isAlive == true)
        {
            rb.velocity = new Vector2(1f * speed, rb.velocity.y);
        }
    }

    void CheckEnemyDirection()
    {
        if (rb.velocity.x < 0)
        {
            goombaFacingLeft = true;
        }
        else if (rb.velocity.x > 0)
        {
            goombaFacingLeft = false;
        }

        if (goombaFacingLeft == true)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
        else if (goombaFacingLeft == false)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy Hitbox" && collision.gameObject.layer != 2)
        {
            speed = -speed;
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
        StartCoroutine(WaitUntilSetInactive(1f));
        StopCoroutine(WaitUntilSetInactive(1f));
    }

    public override void ResetPositionAfterPlayerDeath()
    {
        gameObject.SetActive(true);
        rb.simulated = true;
        isAlive = true;
        enemyCollider.enabled = true;
        transform.position = initialSpawnPosition;

        if (speed != speedAtSpawn)
        {
            speed = speedAtSpawn;
        }
        //add reset look direction function here
    }
}
