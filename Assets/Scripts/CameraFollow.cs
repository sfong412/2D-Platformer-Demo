using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
public class CameraFollow : MonoBehaviour
{
    private PlayerMovement movement;

    public Transform target;

    public GameObject currentLevelBounds;

    public Vector2 cameraSize;

    private Vector2 windowResolution;

    public Vector3 offset;

    private Vector3 initial;

    private Camera camera;

    public PixelPerfectCamera pixelPerfectCamera;

    private SpriteRenderer levelBox;

    private float leftPivot;
    private float rightPivot;
    private float topPivot;
    private float botPivot;

    [Range(1, 10)]
    public float smoothFactor;

    void Awake()
    {
        currentLevelBounds = GameObject.Find("Bounds 1");
        
        camera = GetComponent<Camera>();
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        levelBox = currentLevelBounds.GetComponent<SpriteRenderer>();

        cameraSize = new Vector2((2f * camera.orthographicSize) * camera.aspect, 2f * camera.orthographicSize);

        windowResolution = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        CalculateCameraPivot();

        if (windowResolution.x != Screen.width || windowResolution.y != Screen.height)
        {
            windowResolution.x = Screen.width;
            windowResolution.y = Screen.height;

            cameraSize = new Vector2((2f * camera.orthographicSize) * camera.aspect, 2f * camera.orthographicSize);
        }
    }

    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition;
        float targetY;
/*
        if (target.position.y < 1.5f)
        {
            targetPosition = new Vector3(target.position.x, -2f, target.position.z);
        }
        else
        {
            targetPosition = target.position;
        }
*/

        targetPosition = target.position;

        if (target.position.y < levelBox.bounds.min.y + cameraSize.y)
        {
            targetY = levelBox.bounds.min.y;
        }
        else
        {
            targetY = target.position.y;
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, targetY, targetPosition.z), (smoothFactor * Time.fixedDeltaTime));

        transform.position = new Vector3(Mathf.Clamp(smoothedPosition.x, leftPivot, rightPivot), Mathf.Clamp(smoothedPosition.y, botPivot, topPivot), -0.3f);
    }

     void CalculateCameraPivot()
     {
        botPivot = levelBox.bounds.min.y + cameraSize.y/2;
        topPivot = levelBox.bounds.max.y - cameraSize.y/2;
        leftPivot = levelBox.bounds.min.x + cameraSize.x/2;
        rightPivot = levelBox.bounds.max.x - cameraSize.x/2;
    }

    public void RecalculateBounds(GameObject newBoxBounds)
    {
        Transform transform = movement.GetComponent<Transform>();

        currentLevelBounds = newBoxBounds;
        levelBox = newBoxBounds.GetComponent<SpriteRenderer>();
    }
}
