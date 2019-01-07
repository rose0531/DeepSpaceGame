using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public Transform shootingPoint;
    public GameObject bullet;
    public GameObject rocket;

	
	// Update is called once per frame
	void Update () {
        Vector2 distance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotate = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotate);

        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(rocket, shootingPoint.position, shootingPoint.rotation);
        }
	}
}
