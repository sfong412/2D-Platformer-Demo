using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isUsed;

    Transform transform;

    Vector3 startPosition;

    public GameObject levelBoundsLocation;

    string flagNames = "flag";

    private SpriteRenderer spriteRenderer;

    private Sprite[] sprites;

    void Start()
    {
        isUsed = false;
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(flagNames);
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = startPosition + new Vector3(0.0f, Mathf.Sin(Time.time)/4, 0.0f);
    }

    public void changeSprite(string itemType)
    {
        if (itemType == "Checkpoint")
        {
            spriteRenderer.sprite = sprites[3];
        }

        if (itemType == "End Flag")
        {
            spriteRenderer.sprite = sprites[1];
        }
    }
}
