using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    //Ship managing variables
    [SerializeField]
    private GameObject[] ships; //the index refers to the ships level
    [SerializeField]
    private Transform oceanSurface;
    public Vector2 spawnDistance = new Vector2(-60, 60f);
    public Vector2 oceanClamp = new Vector2(-70, 70);
    public int maxShips = 3;
    private int numOfShips = 0;

    [Space]
    [Space]

    //temporary player variables
    public GameObject player;
    //private PlayerController player;
    public float playerXP; //this will be removed and instead refer to player XP in the player class
    public float distanceFromPlayer = 20f;

    //fixing variables (send help)
    private float[] lastPositions = new float[10];
    private int positionIdex = 0;



    [Space]
    [Space]

    //testinb variables
    public bool sendEvent = false;
    private bool eventSent = false;




    // Start is called before the first frame update
    void Start()
    {
        AddShips();
    }

    // Update is called once per frame
    void Update()
    {
        if(sendEvent != eventSent)
        {
            SpawnNewShip();
            eventSent = sendEvent;
        }
            
    }


    /* This method spawns the max
     * ammount of ships allowed */
    private void AddShips()
    {
        while(numOfShips < maxShips)
        {
            SpawnNewShip();
        }
    }



    /* This method spawns a new ship
     * based on the player's level. */
    private void SpawnNewShip()
    {
        //which ship to spawn
        int randomRange = Random.Range((int)(FishPlayer.instance.strength - 2), (int)(FishPlayer.instance.strength + 1 ));
        int shipIndex = Mathf.Clamp(randomRange, 0, ships.Length - 1);
        //Debug.Log(shipIndex);
        //where to spawn the ship
        float xPos = 1;
        //check which shore it's farther from
        float leftDistance = Mathf.Abs(player.transform.position.x - oceanClamp.y);
        float rightDistance = Mathf.Abs(player.transform.position.x - oceanClamp.x);

        //chose which side of the player to spawn the new boat
        if (leftDistance - rightDistance < 15)
        {
            xPos = Random.Range(0, 100) < 50 ? -1:1;
        }
        else if(leftDistance > rightDistance)
        {
            xPos = -1;
        }

        int loop = 0;
        bool nearLast = true;

        //make sure boat doesnt spawn too close to another spawned boat
        while(nearLast && loop < 100)
        {
            //finalize spawn position
            xPos = distanceFromPlayer + xPos * Random.Range(player.transform.position.x,
                                                            spawnDistance.y);

            //Debug.Log("random xpos found: " + xPos);

            xPos = Mathf.Clamp(xPos, spawnDistance.x, spawnDistance.y);

            //Debug.Log("random xpos post clamp: " + xPos);

            nearLast = false;
            for(int i = 0; i < lastPositions.Length; i++)
            {
                if(Mathf.Abs(lastPositions[i] - xPos) < 10)
                {
                    nearLast = true;
                    break;
                }
            }
            loop++;
        }

        if (!nearLast) Debug.Log("found distant xpos: " + xPos);
        else Debug.Log("did NOT find proper xpos: " + xPos);

        lastPositions[positionIdex] = xPos;
        positionIdex = (positionIdex + 1) % lastPositions.Length;


        Vector3 boatSpawnPos = new Vector3(xPos, oceanSurface.position.y, 0);
        GameObject newShip = Instantiate(ships[shipIndex], boatSpawnPos, ships[shipIndex].transform.rotation);
        newShip.GetComponent<FishingBoat>().InstantiateBoat(oceanClamp, spawnDistance);

        numOfShips++;
    }


    /* This method removes a ship from
     * the counter And changes the max
     * ships in the ocean if needed. */
    public void RemoveShip()
    {
        //hard coded that the max num of ships caps out at 15
        if(maxShips < 15 && maxShips + 1 < playerXP)
        {
            maxShips++;
        }
        numOfShips--;
        AddShips();
    }
}
