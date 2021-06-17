using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallGun : Enemy
{
    public GameObject bullet;

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        WaitUntilShoot(1f);
      //  Shoot();
    }

    void Shoot()
    {
        Instantiate(bullet, transform);
        Debug.Log("shoot the gun");
    }

    IEnumerator WaitUntilShoot(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Shoot();
    }
}
