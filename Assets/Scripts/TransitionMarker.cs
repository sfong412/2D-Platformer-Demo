using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionMarker : MonoBehaviour
{
    public BoxCollider2D boxCollider;

    BoxCollider2D playerCollider;


    public GameObject boundsA;
    public GameObject boundsB;

    public GameObject targetBounds;

    public enum TransitionOrientation {Horizontal, Vertical};
    public enum TransitionDirection { Left , Right, Up, Down };
    public bool transitionState;

    public bool initialTriggerState;
    public bool currentTriggerState;

    CameraFollow cameraFollow;

    [SerializeField] TransitionOrientation orientation = TransitionOrientation.Horizontal;
    [SerializeField] TransitionDirection direction = TransitionDirection.Right;

    // Start is called before the first frame update
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();

        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();

        initialTriggerState = GetComponent<BoxCollider2D>().isTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        GetTargetBounds();
    //    DetectWhichSideIsTouched();
    }

    void GetTargetBounds()
    {/*
        if (cameraFollow.currentLevelBounds == boundsA)
        {
            targetBounds = boundsB;
        }
        else if (cameraFollow.currentLevelBounds == boundsB)
        {
            targetBounds = boundsA;
        }
        */
        targetBounds = boundsB;
    }
    /*
    public void DetectWhichSideIsTouched(Collider2D collider)
    {
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0f, Vector2.left, 20f);

        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0f, Vector2.right, 20f);

        Vector3 normalLeft = raycastHitLeft.normal;
        Vector3 normalRight = raycastHitRight.normal;

        normalLeft = raycastHitLeft.transform.TransformDirection(normalLeft);
        normalRight = raycastHitRight.transform.TransformDirection(normalRight);

        if (normalLeft == -raycastHitLeft.transform.right && raycastHitLeft.collider == collider)
        {
            Debug.Log("touched player on the left");
        }

        if (normalRight == raycastHitRight.transform.right && raycastHitRight.collider == collider)
        {
            Debug.Log("touched player on the right");
        }
    }
*/
    public void ResetTriggerState()
    {
        initialTriggerState = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Vector3 direction = transform.TransformDirection(Vector2.left) * (1 + .5f);
       // Gizmos.DrawWireCube(transform.position, new Vector3(boxCollider.size.x, boxCollider.size.y, 1));
    } 
}
