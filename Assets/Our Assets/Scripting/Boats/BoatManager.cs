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
    public Vector2 spawnDistance = new Vector2(30f, 60f);
    public Vector2 spawnClamp = Vector2.zero;
    public int maxShips = 3;
    private int numOfShips = 0;

    [Space]
    [Space]

    //temporary player variables
    public GameObject player;
    //private PlayerController player;
    public float playerXP; //this will be removed and instead refer to player XP in the player class


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
        int randomRange = Random.Range((int)(playerXP - 4), (int)(playerXP + 2));
        int shipIndex = (int)Mathf.Clamp(randomRange, 0, ships.Length - 1);

        //where to spawn the ship
        float xPos = 1;
        //check which shore it's farther from
        float leftDistance = Mathf.Abs(player.transform.position.x - spawnClamp.y);
        float rightDistance = Mathf.Abs(player.transform.position.x - spawnClamp.x);

        //chose which side of the player to spawn the new boat
        if (leftDistance - rightDistance < 15)
        {
            xPos = Random.Range(0, 100) < 50 ? -1:1;
        }
        else if(leftDistance > rightDistance)
        {
            xPos = -1;
        }

        //finalize spawn position
        xPos = player.transform.position.x + xPos * Random.Range(spawnDistance.x,
                                                                 spawnDistance.y);
        xPos = Mathf.Clamp(xPos, spawnClamp.x, spawnClamp.y);

        Vector3 boatSpawnPos = new Vector3(xPos, oceanSurface.position.y, 0);
        Instantiate(ships[shipIndex], boatSpawnPos, ships[shipIndex].transform.rotation);

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
