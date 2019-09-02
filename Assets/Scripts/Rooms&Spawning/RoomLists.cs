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

    public List<GameObject> closedRoomsList;                    // List of rooms that spawn to close off the level.

    public float waitTime;                                      // Time to wait before spawning the next room.
    public bool closeAllRooms = false;                          // Flag to tell RoomSpawner to start closing the level.
    private bool spawnedTriggers = false;                       // Flag set when triggers are spawned.
    private int rand = 0;                                       // Variable to hold random integer.


    private void Update()
    {
        if(waitTime <= 0)
        {
            closeAllRooms = true;
            if(closedRoomsList.Count > 0 && !spawnedTriggers)
            {
                rand = Random.Range(0, closedRoomsList.Count - 1);
                SpawnObject(Resources.Load("Prefab/Trigger") as GameObject, closedRoomsList[rand].transform.position);
                closedRoomsList.Remove(closedRoomsList[rand]);

                rand = Random.Range(0, closedRoomsList.Count - 1);
                SpawnObject(Resources.Load("Prefab/Trigger") as GameObject, closedRoomsList[rand].transform.position);
                closedRoomsList.RemoveRange(0, closedRoomsList.Count - 1);

                spawnedTriggers = true;
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    public void SpawnObject(GameObject obj, Vector3 pos)
    {
        Instantiate(obj, pos, obj.transform.rotation);
    }
}
