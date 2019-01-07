using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    public GameObject[] objects;
    public GameObject[] cosmetics;
    private GameObject instance;

	// Use this for initialization
	private void Start () {
        int randObj = Random.Range(0, objects.Length);
        instance = (GameObject)Instantiate(objects[randObj], transform.position, Quaternion.identity);
        instance.transform.parent = transform;

        //TODO: instantiate cosmetics
        /*
        if(cosmetics.Length > 0)
        {
            int randCos = 0;
            if(objects[randObj].tag == "Ground")
            {
                randCos = Random.Range(0, cosmetics.Length);
                instance = (GameObject)Instantiate(cosmetics[randCos], new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
                instance.transform.parent = transform;
            }
            
            if(objects[randObj].tag == "Nothing")
            {
                randCos = Random.Range(0, cosmetics.Length);
                instance = (GameObject)Instantiate(cosmetics[randCos], transform.position, Quaternion.identity);
                instance.transform.parent = transform;
            }
        }
        */
	}

    private void OnDrawGizmos()
    {
        for(int i = 0; i < objects.Length; i++)
        {
            if (objects[i].tag == "Enemy")
                Gizmos.color = Color.red;
            else if (objects[i].tag == "AcidCeiling" || objects[i].tag == "AcidGround")
                Gizmos.color = Color.magenta;
            else
                Gizmos.color = Color.white;
        }

        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
