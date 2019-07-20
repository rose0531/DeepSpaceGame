using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwRoom : MonoBehaviour {

    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if(roomDetection == null && levelGen.stopGeneration == true)
        {
            //spawn random room
            int rand = Random.Range(0, levelGen.allRooms.Length);
            Instantiate(levelGen.allRooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
	}
}
