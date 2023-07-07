using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBoat : MonoBehaviour
{
    //manager variables
    private BoatManager boatManager;


    //sinking variables
    public float maxFallSpeed = 10f;
    public float fallAcceleration = 1f;
    private float fallingSpeed = 0;

    public Vector3 finalRotation = new Vector3(30, -10, 30);
    public float rotationAcceleration = 1f;


    //event management variables
    public bool boatPulledUnder = false;
    public float despawnHeight = -20f;


    // Start is called before the first frame update
    void Start()
    {
        boatManager = GameObject.Find("Boat Manager").GetComponent<BoatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //for testing
        if(boatPulledUnder)
        {
            Sink();
        }
    }

    private void Sink()
    {
        //falling down
        fallingSpeed = fallingSpeed < maxFallSpeed ? fallingSpeed + fallAcceleration * Time.deltaTime : maxFallSpeed;
        fallingSpeed *= Time.deltaTime;
        transform.position = new Vector3(transform.position.x,
                                         transform.position.y - fallingSpeed,
                                         transform.position.z);

        //rotating down
        if(Vector3.Distance(transform.rotation.eulerAngles, finalRotation) < 1)
        {
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles,
                                                               finalRotation,
                                                               rotationAcceleration * Time.deltaTime));
        }

        //despawn boat
        if(transform.position.y < despawnHeight)
        {
            boatManager.RemoveShip();
            Destroy(this.gameObject);
        }
    }
}
