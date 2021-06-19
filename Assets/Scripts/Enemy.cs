using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform transform;
    public Rigidbody2D rb;

    public BoxCollider2D enemyCollider;

    public Animator animator;

    public bool isAlive = true;

    public int damageValue;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        enemyCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void OnBecameVisible()
    {
      //  gameObject.SetActive(true);
    }

    void OnBecameInvisible()
    {
       // gameObject.SetActive(false);
    }

    public virtual void Die()
    {
        animator.SetBool("isDead", true);
        isAlive = false;
        //rb.velocity = new Vector2(0f, 0f);
        rb.simulated = false;
        enemyCollider.enabled = false;
       // wallCollider.enabled = false;
        StartCoroutine(WaitUntilSetInactive(1f));
        StopCoroutine(WaitUntilSetInactive(1f));
    }

    public IEnumerator WaitUntilSetInactive(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
    }

    
}
