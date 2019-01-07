using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpawn : MonoBehaviour {

    public GameObject acid;
    public float acidDripRate;
    private float acidDripRateCounter;
    private int randomDripTime;

	// Use this for initialization
	void Start () {
        acidDripRate = acidDripRate * 60; //convert seconds to frames
        acidDripRateCounter = 0;
        randomDripTime = Random.Range(0, 10) * 60;
	}
	
	// Update is called once per frame
	void Update () {
		if(acidDripRateCounter <= 0 && randomDripTime <= 0)
        {
            randomDripTime = 0;
            acidDripRateCounter = acidDripRate;
            Instantiate(acid, transform.position, Quaternion.identity);
        }

        randomDripTime--;
        acidDripRateCounter--;
	}
}
