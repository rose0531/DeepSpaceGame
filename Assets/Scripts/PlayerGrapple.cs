using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapple : MonoBehaviour {

    private DistanceJoint2D joint;
    private RaycastHit2D raycastHit;
    public GameObject grappleHookPrefab;
    private GameObject grappleHook;

    private Vector2 distance;
    public float maxGrappleDistance;
    public LayerMask whatIsGrapplable;
    public LineRenderer grappleLine;
    public float grappleRetractionSpeed;
    public float grapplePadding;
    public float grappleCooldown;
    private float grappleCooldownCounter;

	// Use this for initialization
	void Start () {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        grappleLine.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && grappleCooldownCounter <= 0)
        {
            grappleCooldownCounter = grappleCooldown;

            /* Get distance between Grapple transform and Mouse */
            distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            /* Raycast from Grapple transform to Mouse position */
            raycastHit = Physics2D.Raycast(transform.position, distance, maxGrappleDistance, whatIsGrapplable);
            if (raycastHit.collider != null)
            {
                /* Enable joint and anchor the end of the joint to where the raycast hit the collider */
                joint.enabled = true;
                joint.connectedAnchor = raycastHit.point;

                /* Spawn grapple line renderer */
                grappleLine.enabled = true;
                grappleLine.SetPosition(0, transform.position);     // Starting position of the grapple will be on the player
                grappleLine.SetPosition(1, joint.connectedAnchor);  // Ending position of the grapple will be where our anchor connected

                /* Spawn grapple hook sprite */
                float rotateHook = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
                grappleHook = Instantiate(grappleHookPrefab, joint.connectedAnchor, Quaternion.Euler(0f, 0f, rotateHook));
            }
        }

        if (Input.GetMouseButton(0))
        {
            grappleLine.SetPosition(0, transform.position);
            if(joint.distance > grapplePadding)
            {
                joint.distance -= grappleRetractionSpeed;
            }
            else
            {
                grappleLine.enabled = false;
                joint.enabled = false;
                Destroy(grappleHook);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
            grappleLine.enabled = false;
            Destroy(grappleHook);
        }

        grappleCooldownCounter--;
        
	}
}
