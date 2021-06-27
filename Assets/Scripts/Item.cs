using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isUsed;

    Transform transform;

    Vector3 startPosition;

    public GameObject levelBoundsLocation;

    void Start()
    {
        isUsed = false;
        transform = GetComponent<Transform>();
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = startPosition + new Vector3(0.0f, Mathf.Sin(Time.time)/4, 0.0f);
    }
}
