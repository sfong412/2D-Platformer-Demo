using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallGun : Enemy
{
    public GameObject bullet;
    private Vector3 bulletStartingPoint;

    private float timer = 3f;

    private PlayerMovement player;

    private CameraFollow camera;

    public GameObject enemyLocation;

    AudioSource audio;

    void Start()
    {
        transform = GetComponent<Transform>();

        bulletStartingPoint = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        damageValue = -1;

        camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.currentLevelBounds == enemyLocation)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Instantiate(bullet, bulletStartingPoint, Quaternion.identity, transform);
            audio.Play();
            timer = 1.5f;
        }
    }

    void CheckIfPlayerIsInBounds()
    {
        if (camera.currentLevelBounds == enemyLocation)
        {

        }
    }

    public void ResetPositionAfterPlayerDeath()
    {
        
    }
}
