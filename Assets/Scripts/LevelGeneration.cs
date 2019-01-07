using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    public GameObject cameraHolder;

    public Transform[] startingPosition;
    public GameObject[] allRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    private GameObject lastRoom;

    private int direction;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX, maxX, minY;
    public bool stopGeneration;

    private int downCounter;
    private bool leftCorner;

    public LayerMask room;
    public GameObject goalLights;
    private bool goalLightsOn;

	// Use this for initialization
	private void Start () {
        /* Pick one of five random starting positions */
        int randStartPos = Random.Range(0, startingPosition.Length);

        /* Set LevelGeneration object position to the random starting position */
        transform.position = startingPosition[randStartPos].position;

        /* Instantiate LR room at the random starting position */
        GameObject room = Instantiate(leftRooms[0], transform.position, Quaternion.identity);

        /* Spawn player in starting room */
        PlayerManager.instance.player = room.GetComponent<SpawnPlayer>().Spawn();

        /* Set camera to focus on spawned player */
        cameraHolder.GetComponent<CameraController>().target = PlayerManager.instance.player.transform;

        /* Pick random direction to spawn next room */
        direction = Random.Range(1, 6);

        goalLightsOn = false;
	}

    private void Update()
    {
        /* Wait, and then spawn next room */
        if(timeBtwRoom <= 0 && !stopGeneration)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else if (stopGeneration && !goalLightsOn)
        {
            /* Spawn goal glow effect */
            GameObject lights = Instantiate(goalLights, lastRoom.GetComponent<Room>().goal.transform.position, Quaternion.identity);
            lights.transform.parent = lastRoom.GetComponent<Room>().goal.transform;
            goalLightsOn = true;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if(direction == 1 || direction == 2) // move right
        {
            if(transform.position.x < maxX)
            {
                leftCorner = false;
                downCounter = 0;
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                int type = roomDetection.GetComponent<Room>().type;
                if (type == 4 || type == 6)
                {
                    roomDetection.GetComponent<Room>().RoomDestruction();
                    Instantiate(leftRooms[2], transform.position, Quaternion.identity); // spawn 4 way opening
                }

                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, leftRooms.Length);
                lastRoom = Instantiate(leftRooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 3);
                if(direction == 3)
                {
                    direction = 2;
                }else if(direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
            
        }else if(direction == 3 || direction == 4) // move left
        {
            if(transform.position.x > minX)
            {
                leftCorner = true;
                downCounter = 0;
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                int type = roomDetection.GetComponent<Room>().type;
                if (type == 5 || type == 6)
                {
                    roomDetection.GetComponent<Room>().RoomDestruction();
                    Instantiate(leftRooms[2], transform.position, Quaternion.identity); //spawn 4 way opening
                }

                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rightRooms.Length);
                lastRoom = Instantiate(rightRooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
            
        }else if(direction == 5) // move down
        {
            downCounter++;
            if(transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                int type = roomDetection.GetComponent<Room>().type;
                if (type == 0 || type == 2)
                {
                    roomDetection.GetComponent<Room>().RoomDestruction();
                    if (downCounter >= 2)
                    {
                        Instantiate(leftRooms[2], transform.position, Quaternion.identity); //spawn 4 way opening
                    }
                    else
                    {
                        int randBottomRoom = Random.Range(0, bottomRooms.Length);
                        if (leftCorner && (randBottomRoom == 2 || randBottomRoom == 4))
                            Instantiate(bottomRooms[0], transform.position, Quaternion.identity);
                        else if (!leftCorner && (randBottomRoom == 3 || randBottomRoom == 4))
                            Instantiate(bottomRooms[0], transform.position, Quaternion.identity);
                        else
                            Instantiate(bottomRooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(0, topRooms.Length);
                lastRoom = Instantiate(topRooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                stopGeneration = true;
            }
            
        }
    }
}
