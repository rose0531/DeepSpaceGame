using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour {

    private float length;
    private float startPos;
    public Transform follow = null;
    [SerializeField] private float parallaxEffect;

	void Start () {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	void FixedUpdate () {
        if (follow != null)
        {
            float temp = follow.position.x * (1 - parallaxEffect);
            float dist = follow.position.x * parallaxEffect;

            transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

            if (temp > startPos + length)
                startPos += length;
            else if (temp < startPos - length)
                startPos -= length;
        }
	}
}
