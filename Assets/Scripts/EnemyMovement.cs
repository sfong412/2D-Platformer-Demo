using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Enemy enemy;
    //Animator animator;
   // Rigidbody2D rb;
    BoxCollider2D wallCollider;

    bool enemyFacingRight;

    public bool isAlive = true;

    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
      //  animator = GetComponent<Animator>();
        wallCollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // Move();
    }

    void Move()
    {
        if (isAlive == true)
        {
            enemy.rb.velocity = new Vector2(1f * speed, enemy.rb.velocity.y);
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
/*
    public void Die()
    {
        enemy.animator.SetBool("isDead", true);
        isAlive = false;
        //rb.velocity = new Vector2(0f, 0f);
        enemy.rb.simulated = false;
        enemy.enemyCollider.enabled = false;
        wallCollider.enabled = false;
        StartCoroutine(WaitUntilSetInactive());
        StopCoroutine(WaitUntilSetInactive());
    }

    IEnumerator WaitUntilSetInactive()
    {
        yield return new WaitForSecondsRealtime(1f);
        gameObject.SetActive(false);
    }
    */
}
