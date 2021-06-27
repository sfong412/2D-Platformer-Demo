using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerHealth health;
    PlayerMovement movement;
    CameraFollow camera;
    Animator playerAnimator;

    BoxCollider2D boxCollider2D;
    public Collider2D previousTransitionMarker;

    public Collider2D testTransitionMarker;
    Level level;

    UI ui;
    public bool standingOnSpikes;

   // private float checkpointAlertTimer = 0f;

   // GameObject checkpointText;

    bool transition = false;

    void Awake()
    {
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        level = GameObject.Find("Gameplay Manager").GetComponent<Level>();
        ui = GameObject.Find("UI").GetComponent<UI>();
    }

    void Start()
    {
       // checkpointText = GameObject.Find("Checkpoint Text");
    }

    void Update()
    {
        SpikeCheck();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Enemy currentEnemy = collision.gameObject.GetComponentInParent<Enemy>();

        if (collision.gameObject.tag == "Spikes")
        {
            health.ChangeHealth(collision.gameObject.GetComponent<Spikes>().damageValue);
            standingOnSpikes = true;
        }

        if (collision.gameObject.tag == "Enemy Hitbox" && currentEnemy.isAlive == true)
        {
            health.ChangeHealth(currentEnemy.damageValue);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            standingOnSpikes = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Item currentItem = collision.gameObject.GetComponent<Item>();
        TransitionMarker currentTransitionMarker = collision.gameObject.GetComponent<TransitionMarker>();
        Enemy currentEnemy = collision.gameObject.GetComponentInParent<Enemy>();

        if (collision.gameObject.tag == "Checkpoint" && currentItem.isUsed == false)
        {
            movement.currentSpawnPoint = collision.gameObject.transform.position;
            currentItem.isUsed = true;
            health.currentCheckpointLocation = currentItem.levelBoundsLocation;
            ui.onCheckpointUsed();
        }

        if (collision.gameObject.tag == "Health Pickup" && currentItem.isUsed == false)
        {
            health.ChangeHealth(12);
            currentItem.isUsed = true;
        }

        if (collision.gameObject.tag == "End Flag" && currentItem.isUsed == false)
        {
            level.isCompleted = true;
            currentItem.isUsed = true;
        }

        if (collision.gameObject.tag == "Bounds Transition Marker" && currentTransitionMarker.boundsA == camera.currentLevelBounds)
        {
            camera.RecalculateBounds(currentTransitionMarker.targetBounds);
            
            if (collision.isTrigger == true)
            {
                movement.transform.localPosition = new Vector3(movement.transform.localPosition.x + 0.55f,  movement.transform.localPosition.y, movement.transform.localPosition.z);
            }

            previousTransitionMarker = collision;
        }

        if (collision.gameObject.tag == "Enemy Hitbox" && collision.gameObject.layer == 2)
        {
            health.ChangeHealth(-1);
        }
        
        if (collision.gameObject.tag == "Enemy Hurtbox" && currentEnemy.isAlive == true)
        {
            currentEnemy.Die();
            movement.rb.velocity = new Vector2 (movement.rb.velocity.x, 7f);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
       
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bounds Transition Marker")
        {
            collision = previousTransitionMarker;
            collision.isTrigger = false;
        }
    }

    void SpikeCheck()
    {
        if (standingOnSpikes == true)
        {
            health.ChangeHealth(-1);
        }
    }
}
