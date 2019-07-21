using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLists : MonoBehaviour {

    public GameObject[] allRooms;                               // Array of all the room tiles
    public GameObject[] leftRooms;                              // Array of all the room tiles with left opennings
    public GameObject[] rightRooms;                             // Array of all the room tiles with right opennings
    public GameObject[] topRooms;                               // Array of all the room tiles with top opennings
    public GameObject[] bottomRooms;                            // Array of all the room tiles with bottom opennings
    public GameObject blockerRoom;                              // Filled room that blocks other room opennings
    public GameObject closedRoomT;
    public GameObject closedRoomR;
    public GameObject closedRoomB;
    public GameObject closedRoomL;

    public List<GameObject> rooms;

    public float waitTime;
    public bool closeAllRooms = false;

    private void Update()
    {
        if(waitTime <= 0)
        {
            closeAllRooms = true;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
