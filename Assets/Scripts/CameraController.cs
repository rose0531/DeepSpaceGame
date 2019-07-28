using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;                                                        // The point that the camera will follow.
    [SerializeField] bool smoothing = false;                                        // You can turn camera smoothing ON/OFF.
    [SerializeField] private float smoothSpeed = 5f;                                // Speed of the camera when moving.
    [SerializeField] private float focusPointPercentBetweenTargetAndMouse = 0.25f;  /* Focus the camera on a point between the Mouse and Target by
                                                                                       the specified amount.
                                                                                       Example: 0.25f would focus the camera 25% of the distance
                                                                                                between the Mouse and Target.
                                                                                    */

    /* Smoothing the camera in LateUpdate causes a jittery effect to the player sprite.
       FixedUpdate removes any jitters for some reason.
    */
    private void FixedUpdate()
    {
        if(target != null)
        {
            Vector3 mousePosToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 distBetweenMouseAndTarget = mousePosToWorld - target.position;
            Vector3 desiredPosition = (focusPointPercentBetweenTargetAndMouse * distBetweenMouseAndTarget) + target.position;
            if (smoothing)
            {
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition;
            }else
                transform.position = desiredPosition;
        }
    }
}
