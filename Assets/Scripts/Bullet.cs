using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletSpeed;
    public float bulletLifeTime;
    public int damage;

    //public GameObject destroyEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            if(other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
