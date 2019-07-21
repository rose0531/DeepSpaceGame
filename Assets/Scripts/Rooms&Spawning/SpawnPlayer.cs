using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

    private void Start()
    {
        GameObject player = Instantiate(Resources.Load("Prefab/Player") as GameObject, transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>().target = player.transform;
    }
}
