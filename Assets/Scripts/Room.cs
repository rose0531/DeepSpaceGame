using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Room : MonoBehaviour {

    public int type;
    public GameObject goal;

    public void RoomDestruction()
    {
        // Notify child objects that the room is being destroyed.

        // Destroy the room.
        Destroy(gameObject);
    }
}
