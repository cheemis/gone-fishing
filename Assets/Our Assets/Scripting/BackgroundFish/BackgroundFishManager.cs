using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFishManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] fishPrefabs;
    public int maxNumOfFish = 20;
    private int currentFish = 0;
    private int lastFish = -1;

    [Space]
    [Space]

    public float[] percentRanges = { 10f, 45f, 100};

    [Space]
    [Space]

    public Vector2 foregroundRange = new Vector2(-5f, -1f);
    public Vector2 foregroundUpDown = new Vector2(.5f, 6f);
    public Vector2 foregroundSize = new Vector2(.5f, 1f);
    public float foregroundDistance = 15;

    [Space]
    [Space]

    public Vector2 backgroundRange = new Vector2(4f, 17.5f);
    public Vector2 backgroundUpDown = new Vector2(2f, 9f);
    public Vector2 backgroundSize = new Vector2(1f, 3f);
    public float backgroundDistance = 30;

    [Space]
    [Space]

    public Vector2 wayBackgroundRange = new Vector2(30f, 100f);
    public Vector2 wayBackgroundUpDown = new Vector2(5f, 15f);
    public Vector2 wayBackgroundSize = new Vector2(2f, 8f);
    public float wayBackgroundDistance = 90;

    // Start is called before the first frame update
    void Start()
    {
        SpawnFish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnFish()
    {
        while(currentFish < maxNumOfFish)
        {
            ChooseFishLocation();
        }
    }

    private void ChooseFishLocation()
    {
        int location = Random.Range(0, 100);

        if(location < percentRanges[0])
        {
            SpawnRandomFish(Random.Range(foregroundRange.x, foregroundRange.y),
                            Random.Range(foregroundUpDown.x, foregroundUpDown.y),
                            Random.Range(foregroundSize.x, foregroundSize.y),
                            foregroundDistance);
        }
        else if(location < percentRanges[1])
        {
            SpawnRandomFish(Random.Range(backgroundRange.x, backgroundRange.y),
                            Random.Range(backgroundUpDown.x, backgroundUpDown.y),
                            Random.Range(backgroundSize.x, backgroundSize.y),
                            backgroundDistance);
        }
        else
        {
            SpawnRandomFish(Random.Range(wayBackgroundRange.x, wayBackgroundRange.y),
                            Random.Range(wayBackgroundUpDown.x, wayBackgroundUpDown.y),
                            Random.Range(wayBackgroundSize.x, wayBackgroundSize.y),
                            wayBackgroundDistance);
        }
    }

    private void SpawnRandomFish(float range, float upDown, float size, float distanceFromPlayer)
    {
        int randFish = Random.Range(0, fishPrefabs.Length);
        if (randFish == lastFish) randFish = (randFish + 1) % fishPrefabs.Length;

        float dir = Random.Range(0, 100) < 50 ? -1 : 1; 

        Vector3 spawnLocation = new Vector3(player.transform.position.x + -dir * distanceFromPlayer,
                                            upDown,
                                            range);

        GameObject newFish = Instantiate(fishPrefabs[randFish],
                                         spawnLocation,
                                         fishPrefabs[randFish].transform.rotation);

        newFish.transform.localScale = new Vector3(size * dir,
                                                   size,
                                                   size);

        newFish.GetComponent<BackgroundFish>().InstantiateFish(dir,
                                                               player,
                                                               distanceFromPlayer,
                                                               this);

        currentFish++;
    }


    public void RemoveFish()
    {
        currentFish--;
        SpawnFish();
    }
}
