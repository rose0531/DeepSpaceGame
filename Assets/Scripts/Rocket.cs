using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public float rocketSpeed;
    public float rocketLifeTime;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * rocketSpeed * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
