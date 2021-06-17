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
    public bool standingOnSpikes;

    bool transition = false;

    void Awake()
    {
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        level = GameObject.Find("Gameplay Manager").GetComponent<Level>();
    }

    void Start()
    {

    }

    void Update()
    {
        SpikeCheck();
       // BlockPreviousRoomFromEntry();

        //Debug.Log(boxCollider2D.Distance(testTransitionMarker).distance);

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Enemy currentEnemy = collision.gameObject.GetComponentInParent<Enemy>();

        if (collision.gameObject.tag == "Spikes")
        {
            health.ChangeHealth(-1);
            standingOnSpikes = true;
        }

        if (collision.gameObject.tag == "Enemy Hitbox" && currentEnemy.isAlive == true)
        {
            Debug.Log("touched");
            health.ChangeHealth(-1);
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
            movement.currentSpawnPoint = collision.gameObject.transform.localPosition;
            currentItem.isUsed = true;
            health.currentCheckpointLocation = currentItem.levelBoundsLocation;
            Debug.Log(currentItem);
        }

        if (collision.gameObject.tag == "Health Pickup" && currentItem.isUsed == false)
        {
            health.ChangeHealth(12);
            currentItem.isUsed = true;
        //    Debug.Log("health pickup used");
        }

        if (collision.gameObject.tag == "End Flag" && currentItem.isUsed == false)
        {
            level.isCompleted = true;
            currentItem.isUsed = true;
         //   Debug.Log("end flag used");
        }

        if (collision.gameObject.tag == "Bounds Transition Marker" && currentTransitionMarker.boundsA == camera.currentLevelBounds)
        {
            camera.RecalculateBounds(currentTransitionMarker.targetBounds);
            
            if (collision.isTrigger == true)
            {
                movement.transform.localPosition = new Vector3(movement.transform.localPosition.x + 0.55f,  movement.transform.localPosition.y, movement.transform.localPosition.z);
            }

            previousTransitionMarker = collision;
            //add camera transition function here
        }

       // if (collision.gameObject.tag == "Enemy Hitbox" && currentEnemy.isAlive == true)
      //  {
          //  health.ChangeHealth(-1);
      //  }
        
        if (collision.gameObject.tag == "Enemy Hurtbox" && currentEnemy.isAlive == true)
        {
          // Debug.Log(currentEnemy);
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
        //    Debug.Log("bounds Transition marker exited");
        }
    }

    void BlockPreviousRoomFromEntry()
    {
        if (previousTransitionMarker == null)
        {
            return;
        }

       // if (previousTransitionMarker.Distance(boxCollider2D).distance >= 0.5f)
      //  {
            previousTransitionMarker.isTrigger = false;
     //   }
    }
    void SpikeCheck()
    {
        if (standingOnSpikes == true)
        {
          //  playerAnimator.SetTrigger("Hurt");
            health.ChangeHealth(-1);
        }
    }
}
