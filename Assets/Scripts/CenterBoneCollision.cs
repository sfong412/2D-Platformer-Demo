using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterBoneCollision : MonoBehaviour
{
    CameraFollow camera;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        TransitionMarker currentTransitionMarker = collision.gameObject.GetComponent<TransitionMarker>();

        if (collision.gameObject.tag == "Bounds Transition Marker")
        {
            if (!currentTransitionMarker.transitionState)
            {
                currentTransitionMarker.transitionState = true;
            }
            camera.RecalculateBounds(currentTransitionMarker.targetBounds);

            Debug.Log("bounds Transition marker used");
            //add camera transition function here
        }
    }
}
