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
    private GameObject room;

    private void Start()
    {
        rooms = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomLists>();
        Invoke("SpawnRoom", roomSpawnTime); // Call SpawnRoom every specified amount of seconds
    }

    // Update is called once per frame
    void SpawnRoom () {
        if (!spawned)
        {
            if (openingDirection == OpeningDirection.top)
            {
                // Spawn room with top door
                if (rooms.closeAllRooms == false)
                {
                    rand = Random.Range(0, rooms.topRooms.Length);
                    room = Instantiate(rooms.topRooms[rand], transform.position, rooms.topRooms[rand].transform.rotation);
                }
                else
                {
                    // Spawn a closed room. (AKA - A room with one openning)
                    room = Instantiate(rooms.closedRoomT, transform.position, rooms.closedRoomT.transform.rotation);

                    // Add the room to our closed room list.
                    rooms.closedRoomsList.Add(room);
                }
            }
            else if (openingDirection == OpeningDirection.right)
            {
                // Spawn room with right door
                if (rooms.closeAllRooms == false)
                {
                    rand = Random.Range(0, rooms.rightRooms.Length);
                    room = Instantiate(rooms.rightRooms[rand], transform.position, rooms.rightRooms[rand].transform.rotation);
                }
                else
                {
                    room = Instantiate(rooms.closedRoomR, transform.position, rooms.closedRoomR.transform.rotation);
                    rooms.closedRoomsList.Add(room);
                }
            }
            else if (openingDirection == OpeningDirection.bottom)
            {
                // Spawn room with bottom door
                if (rooms.closeAllRooms == false)
                {
                    rand = Random.Range(0, rooms.bottomRooms.Length);
                    room = Instantiate(rooms.bottomRooms[rand], transform.position, rooms.bottomRooms[rand].transform.rotation);
                }
                else
                {
                    room = Instantiate(rooms.closedRoomB, transform.position, rooms.closedRoomB.transform.rotation);
                    rooms.closedRoomsList.Add(room);
                }
            }
            else if (openingDirection == OpeningDirection.left)
            {
                // Spawn room with left door
                if (rooms.closeAllRooms == false)
                {
                    rand = Random.Range(0, rooms.leftRooms.Length);
                    room = Instantiate(rooms.leftRooms[rand], transform.position, rooms.leftRooms[rand].transform.rotation);
                }
                else
                {
                    room = Instantiate(rooms.closedRoomL, transform.position, rooms.closedRoomL.transform.rotation);
                    rooms.closedRoomsList.Add(room);
                }
            }
            rooms.allRoomsList.Add(room);
        }
        spawned = true;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<RoomSpawner>() && other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                // Spawn wall to close any opennings
                Instantiate(rooms.blockerRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
