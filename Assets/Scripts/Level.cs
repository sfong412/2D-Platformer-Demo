using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public bool isCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        isCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void CompleteLevel()
    {
        isCompleted = true;
    }

    public void Transition()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();

        if (collision.gameObject.tag == "Enemy")
        {
            enemy.speed = -enemy.speed;
        }
    }

    public void QuitGame()
    {
        Resources.UnloadUnusedAssets();
        Application.Quit();
    }
}
