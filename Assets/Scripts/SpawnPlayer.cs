using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    public Transform spawnPlayerPoint;
    public GameObject player;

	public GameObject Spawn()
    {
        return Instantiate(player, spawnPlayerPoint.position, Quaternion.identity);
    }
}
