using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    PlayerHealth health;
    public int lives;

    bool gameOver;

    void Awake()
    {
        health = GameObject.Find("Player").GetComponent<PlayerHealth>();
        gameOver = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        lives = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
        {
            Debug.Log("Ran out of lives");
        }
    }

    public void LoseLife()
    {
    }
}
