using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Camera Camera { get; private set; }

    public Transform ibb, obb;
    [SerializeField]
    [Tooltip("The distance between the players will be at most this ratio of screen space.")]
    float focusRatio = 0.8f;
    [SerializeField]
    [Tooltip("How smooth the camera refocusses on the players.")]
    float smoothingRate = 0.9f;

    Vector3 smoothIbbPosition, smoothObbPosition;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GetComponent<Camera>();
        smoothIbbPosition = ibb.position;
        smoothObbPosition = obb.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Center camera between players
        transform.position = new Vector3(
            (smoothIbbPosition.x + smoothObbPosition.x) / 2f,
            (smoothIbbPosition.y + smoothObbPosition.y) / 2f,
            transform.position.z
        );

        // Zoom to keep the screen distance between players at most the given ratio of screen width or height
        Vector3 ibbScreenPosition = Camera.WorldToScreenPoint(smoothIbbPosition);
        Vector3 obbScreenPosition = Camera.WorldToScreenPoint(smoothObbPosition);
        float xDifference = Mathf.Abs(ibbScreenPosition.x - obbScreenPosition.x) / Camera.pixelWidth;
        float yDifference = Mathf.Abs(ibbScreenPosition.y - obbScreenPosition.y) / Camera.pixelHeight;
        float maxDifference = Mathf.Max(xDifference, yDifference);
        Camera.orthographicSize *= maxDifference / focusRatio;
        Camera.orthographicSize = Mathf.Max(Camera.orthographicSize, 3f);
    }

    private void FixedUpdate()
    {
        smoothIbbPosition *= smoothingRate;
        smoothObbPosition *= smoothingRate;
        smoothIbbPosition += (1f - smoothingRate) * ibb.position;
        smoothObbPosition += (1f - smoothingRate) * obb.position;
    }
}
