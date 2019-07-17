using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

    public enum OpeningDirection { top, right, bottom, left };

    public OpeningDirection openingDirection;
    private RoomLists rooms;
    private int rand;
    private bool spawned = false;
    private float roomSpawnTime = 0.2f;

    private void Start()
    {
        rooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomLists>();
        Invoke("SpawnRoom", roomSpawnTime); // Call SpawnRoom every specified amount of seconds
    }

    // Update is called once per frame
    void SpawnRoom () {
        if (spawned == false)
        {
            if (openingDirection == OpeningDirection.top)
            {
                // Spawn room with top door
                if (rooms.closeAllRooms == false) {
                    rand = Random.Range(0, rooms.topRooms.Length);
                    Instantiate(rooms.topRooms[rand], transform.position, rooms.topRooms[rand].transform.rotation);
                }
                else
                    Instantiate(rooms.closedRoomT, transform.position, rooms.closedRoomT.transform.rotation);
            }
            else if (openingDirection == OpeningDirection.right)
            {
                // Spawn room with right door
                if (rooms.closeAllRooms == false)
                {
                    rand = Random.Range(0, rooms.rightRooms.Length);
                    Instantiate(rooms.rightRooms[rand], transform.position, rooms.rightRooms[rand].transform.rotation);
                }
                else
                    Instantiate(rooms.closedRoomR, transform.position, rooms.closedRoomR.transform.rotation);
            }
            else if (openingDirection == OpeningDirection.bottom)
            {
                // Spawn room with bottom door
                if (rooms.closeAllRooms == false)
                {
                    rand = Random.Range(0, rooms.bottomRooms.Length);
                    Instantiate(rooms.bottomRooms[rand], transform.position, rooms.bottomRooms[rand].transform.rotation);
                }else
                    Instantiate(rooms.closedRoomB, transform.position, rooms.closedRoomB.transform.rotation);
            }
            else if (openingDirection == OpeningDirection.left)
            {
                // Spawn room with left door
                if (rooms.closeAllRooms == false)
                {
                    rand = Random.Range(0, rooms.leftRooms.Length);
                    Instantiate(rooms.leftRooms[rand], transform.position, rooms.leftRooms[rand].transform.rotation);
                }else
                    Instantiate(rooms.closedRoomL, transform.position, rooms.closedRoomL.transform.rotation);
            }
        }
        spawned = true;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            // TODO: figure out why rooms shift sometimes
            if(other.GetComponent<RoomSpawner>() && other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                // Spawn wall to close any opennings
                Instantiate(rooms.blockerRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
