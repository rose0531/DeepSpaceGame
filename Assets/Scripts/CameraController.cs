using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;            // Target is what the camera will follow
    public float smoothSpeed;           // Speed of the camera when moving
    public Vector3 offset;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    private void LateUpdate()
    {
        if(target != null && transform.position != target.position)
        {
            Vector3 desiredPosition = target.position + offset;
            //desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
            //desiredPosition.y = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
