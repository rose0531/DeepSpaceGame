using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public int type;
    public GameObject goal;

    public void RoomDestruction()
    {
        Destroy(gameObject);
    }
}
