using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    PlayerLives lives;
    PlayerMovement movement;

    SpriteRenderer sprite;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    Animator animator;

    CameraFollow cameraFollow;

    public IEnumerator damageCoroutine;
    IEnumerator respawnCoroutine;

    public int health;
    int startingHealthAmount;

    public bool justGotDamaged = false;
    public bool isInvincible = false;
    public bool isRespawning = false;

    public bool isDead = false;

    public GameObject currentCheckpointLocation;

    public GameObject startingCheckpointLocation;

    GameObject[] enemies;

    AudioSource audio;

    public AudioClip hurtSFX;
    public AudioClip fallSFX;

    void Awake()
    {
        health = 2;
        startingHealthAmount = health;
        lives = GameObject.Find("Gameplay Manager").GetComponent<PlayerLives>();
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (enemies == null)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy Hitbox");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeHealth(int changeAmount)
    {
        if (changeAmount < 0)
        {
            if (isInvincible == false && isDead == false)
            {
                health = health + changeAmount;
                damageCoroutine = GetDamaged(0.5f, 1.5f);
                audio.PlayOneShot(hurtSFX, 0.75f);
                animator.Play("Hurt", 0, 0f);
                animator.SetBool("is Hurt", true);
                StartCoroutine(damageCoroutine);
            }
        }
        else if (changeAmount > 0)
        {
            health = health + changeAmount;
        }
    }

    public IEnumerator GetDamaged(float stun, float invicibility)
    {
        //damage stun state
        isInvincible = true;
        justGotDamaged = true;
        IgnoreCollision(true);

        //flicker / invincibility state
        yield return new WaitForSecondsRealtime(stun);
        justGotDamaged = false;
        animator.SetBool("is Hurt", false);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.8f);

        //invulnerability ended
        yield return new WaitForSecondsRealtime(invicibility);
        isInvincible = false;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        IgnoreCollision(false);
    }
    public void Die()
    {
        GameObject[] transitionMarkers = GameObject.FindGameObjectsWithTag("Bounds Transition Marker");

        if (health <= 0 || transform.position.y <= -75)
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }

            if (transform.position.y <= -75 && isDead == false)
            {
                audio.PlayOneShot(fallSFX, 0.75f);
                isDead = true;
            }
            else
            {
                isDead = true;
            }
            //Debug.Log("Lives: " + lives.lives);
            lives.lives--;
            rb.simulated = false;

            foreach (GameObject transitionMarker in transitionMarkers)
            {
                if (currentCheckpointLocation == transitionMarker.GetComponent<TransitionMarker>().boundsA)
                {
                    transitionMarker.GetComponent<TransitionMarker>().boxCollider.isTrigger = true;
                }
            }

            respawnCoroutine = WaitUntilRespawn(0.8f);
            StartCoroutine(respawnCoroutine);
        }
    }

    IEnumerator WaitUntilRespawn(float seconds)
    {
        isRespawning = true;
        yield return new WaitForSecondsRealtime(seconds);
        Respawn();
        SetEnemiesActiveAfterRespawn();
        isRespawning = false;
    }

    public void Respawn()
    {
        if (currentCheckpointLocation != null)
        {
            cameraFollow.RecalculateBounds(currentCheckpointLocation);
        }
        else
        {
            cameraFollow.RecalculateBounds(startingCheckpointLocation);
        }
        IgnoreCollision(false);
        animator.SetBool("is Hurt", false);
        rb.velocity = new Vector2(0f, 0f);
        transform.position = movement.currentSpawnPoint;
        cameraFollow.Respawn(movement.currentSpawnPoint);
        isDead = false;
        rb.simulated = true;
        justGotDamaged = false;
        isInvincible = false;
        health = startingHealthAmount;
        isRespawning = false;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
    }

    void IgnoreCollision(bool playerIsDamaged)
    {
        BoxCollider2D[] enemy = GameObject.Find("Enemies").GetComponentsInChildren<BoxCollider2D>();

        foreach (BoxCollider2D enemyCollider in enemy)
        {
            Physics2D.IgnoreCollision(boxCollider, enemyCollider, playerIsDamaged);
        }
    }

    void SetEnemiesActiveAfterRespawn()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);

            if (enemy.TryGetComponent(out EnemyGoomba goomba))
            {
                goomba.ResetPositionAfterPlayerDeath();
            }
        }
    }
}
