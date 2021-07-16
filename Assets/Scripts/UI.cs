using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    TextMeshProUGUI lifeUI;

    PlayerHealth health;

    Level level;

    Transform transform;

    GameObject levelComplete;

    GameObject checkpointText;

    float checkpointUITimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        lifeUI = GetComponentInChildren<TextMeshProUGUI>();

        health = GameObject.Find("Player").GetComponent<PlayerHealth>();
        level = GameObject.Find("Gameplay Manager").GetComponent<Level>();
        transform = GetComponent<Transform>();
        levelComplete = transform.Find("Level Complete").gameObject;
        checkpointText = transform.Find("Checkpoint Text").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(health.health);
        lifeUI.text = "Health: " + health.health.ToString();

        if (level.isCompleted == true)
        {
            onLevelComplete();
        }

        if (checkpointUITimer > 0f)
        {
            checkpointText.SetActive(true);
        }
        else
        {
            checkpointText.SetActive(false);
        }

        checkpointUITimer -= Time.deltaTime;
    }

    void onLevelComplete()
    {
        levelComplete.SetActive(true);
    }

    public void onCheckpointUsed()
    {
        checkpointUITimer = 3f;
    }
}
