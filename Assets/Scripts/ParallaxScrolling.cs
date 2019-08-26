using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour {

    private float lengthX, lengthY;
    private float startPosX, startPosY;
    public Transform follow = null;
    [SerializeField] private float parallaxEffectX;
    [SerializeField] private float parallaxEffectY;

	void Start () {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
	}
	
	void FixedUpdate () {
        if (follow != null)
        {
            float leftDist = follow.position.x * (1 - parallaxEffectX);
            float rightDist = follow.position.x * parallaxEffectX;
            float upDist = follow.position.y * parallaxEffectY;
            float downDist = follow.position.y * (1 - parallaxEffectY);
            
            transform.position = new Vector3(startPosX + rightDist, startPosY + downDist, transform.position.z);

            if (leftDist > startPosX + lengthX)
                startPosX += lengthX;
            else if (leftDist < startPosX - lengthX)
                startPosX -= lengthX;
            

        }
	}
}
