using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;                                                        // The point that the camera will follow.
    [SerializeField] bool smoothing = false;                                        // You can turn camera smoothing ON/OFF.
    [SerializeField] private float smoothSpeed = 5f;                                // Speed of the camera when moving.
    [SerializeField] private Vector3 offset;
    [SerializeField] private float focusPointPercentBetweenTargetAndMouse = 0.25f;  /* Focus the camera on a point between the Mouse and Target by
                                                                                       the specified amount.
                                                                                       Example: 0.25f would focus the camera 25% of the distance
                                                                                                between the Mouse and Target.
                                                                                    */
    private float rad = 5.0f;

    /* Smoothing the camera in LateUpdate causes a jittery effect to the player sprite.
       FixedUpdate removes any jitters for some reason.
    */
    private void FixedUpdate()
    {
        //Vector3 c = Camera.main.ScreenToWorldPoint(Input.mousePosition) - target.position;
        //float d = Vector3.Magnitude(c);

        if (target != null)
        {
            Vector3 mousePosToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 distBetweenMouseAndTarget = mousePosToWorld - target.position;
            Vector3 desiredPosition = (focusPointPercentBetweenTargetAndMouse * distBetweenMouseAndTarget) + target.position;
            if (smoothing)
            {
                transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            }else
                transform.position = desiredPosition;
        }
    }
}
