using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Enemy
{
    public float speed;

    private CameraFollow camera;
    private SpriteRenderer levelBox;

    Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        enemyCollider = GetComponent<BoxCollider2D>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        levelBox = camera.currentLevelBounds.GetComponent<SpriteRenderer>();
        speed = 0.005f;

        damageValue = -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + speed, transform.localPosition.y, transform.localPosition.z);

        DestroyIfBeyondBounds();
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            Debug.Log("bullet gone");
        }
    }

    void DestroyIfBeyondBounds()
    {
        if (transform.position.x < levelBox.bounds.min.x || transform.position.x > levelBox.bounds.max.x || transform.position.y < levelBox.bounds.min.y || transform.position.y > levelBox.bounds.max.y)
        {
            Destroy(gameObject);
        }
    }
}
