using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Camera Camera { get; private set; }

    public Transform ibb, obb;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Center camera between players
        transform.position = new Vector3(
            (ibb.position.x + obb.position.x) / 2f,
            (ibb.position.y + obb.position.y) / 2f,
            transform.position.z
        );

        // Zoom to keep the screen distance between players at most 10/12th of screen width or height
        Vector3 ibbScreenPosition = Camera.WorldToScreenPoint(ibb.position);
        Vector3 obbScreenPosition = Camera.WorldToScreenPoint(obb.position);
        float xDifference = Mathf.Abs(ibbScreenPosition.x - obbScreenPosition.x) / Camera.pixelWidth;
        float yDifference = Mathf.Abs(ibbScreenPosition.y - obbScreenPosition.y) / Camera.pixelHeight;
        float maxDifference = Mathf.Max(xDifference, yDifference);
        Camera.orthographicSize *= maxDifference * 1.2f;
        Camera.orthographicSize = Mathf.Max(Camera.orthographicSize, 3f);
    }
}
