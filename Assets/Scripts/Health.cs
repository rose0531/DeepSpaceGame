using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int health;
    public GameObject HUD;

	// Use this for initialization
	void Start () {
        HUD.GetComponent<HUD>().SpawnHealth(health);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int damage)
    {

    }
}
