using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraTransition : MonoBehaviour
{
    CameraFollow cameraFollow;

    Vector3 initialCameraPosition;
    Vector3 initialPlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Transition()
    {
        initialCameraPosition = GameObject.Find("Main Camera").GetComponent<Camera>().transform.position;
        initialPlayerPosition = GameObject.Find("Player").transform.position;
//
     //   targetCameraPosition = 
    }
}
