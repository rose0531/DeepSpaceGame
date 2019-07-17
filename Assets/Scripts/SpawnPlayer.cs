using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    public Transform spawnPlayerPoint;
    public GameObject player;

	public void Spawn()
    {
        player = Instantiate(Resources.Load("Prefab/Player") as GameObject, spawnPlayerPoint.position, Quaternion.identity);
    }
}
