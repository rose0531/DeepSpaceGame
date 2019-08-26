using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    public static SpawnPlayer instance;
    public GameObject player;

    private void Awake()
    {
        // If the instance doesn't exist yet, then assign 'this' script to it.
        if (instance == null)
        {
            instance = this;
        }
        // If the instance is not 'this' instance, then delete this instance.
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        player = Instantiate(Resources.Load("Prefab/Player") as GameObject, transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>().target = player.transform;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("ParallaxBackground");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].GetComponent<ParallaxScrolling>() != null)
            {
                gameObjects[i].GetComponent<ParallaxScrolling>().follow = player.transform;
            }
        }
    }
}
