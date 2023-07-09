using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFish : MonoBehaviour
{
    //movement variables
    public float speed = 1f;
    public float dir = 1;

    //management variables
    private GameObject player;
    private float distanceFromPlayer = 90;
    private BackgroundFishManager manager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + speed * dir,
                                         transform.position.y,
                                         transform.position.z);

        if (Mathf.Abs(transform.position.x - player.transform.position.x) > distanceFromPlayer + 5)
        {
            manager.RemoveFish();
            Destroy(this.gameObject);
        }
    }

    public void InstantiateFish(float dir, GameObject player, float distanceFromPlayer, BackgroundFishManager manager)
    {
        this.dir = dir;
        this.player = player;
        this.distanceFromPlayer = distanceFromPlayer;
        this.manager = manager;

        speed = Random.Range(speed * .5f, speed * 1.5f);
    }
}
