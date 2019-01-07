using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDrip : MonoBehaviour {

    public float dripSpeed;
    public ParticleSystem acidSplash;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.down * dripSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "AcidCeiling")
        {
            if(other.gameObject.tag == "Player")
                PlayerManager.instance.player.GetComponent<Health>().TakeDamage(5);
            Instantiate(acidSplash, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }   
    }
}
