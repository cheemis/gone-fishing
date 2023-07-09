using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBoat : MonoBehaviour
{
    //manager variables
    private BoatManager boatManager;

    //floating variables
    public float fishingSpeed = 5f;
    private bool goingRight = true;
    private Vector2 boatClamps = new Vector2(-30, 30); 

    [Space]
    [Space]
    //spining variables
    public float spinSpeed = 10f;
    private float goalRot = 180;

    [Space]
    [Space]
    //sinking variables
    public float maxFallSpeed = 10f;
    public float fallAcceleration = 1f;
    public float fallingSpeed = 0;

    [Space]
    [Space]
    //rotating variables
    public float rotationAmount = 45f;
    public float rotationAcceleration = 1f;
    private Transform boatAssets;
    private FishingLine line;

    [SerializeField]
    private LureObject lure;

    [Space]
    [Space]
    //event management variables
    public string boatState = "fishing";
    public float despawnHeight = -20f;
    public bool WaitingPostSpin = false;


    // Start is called before the first frame update
    void Start()
    {
        boatManager = GameObject.Find("Boat Manager").GetComponent<BoatManager>();
        boatAssets = transform.GetChild(0);
        line = GetComponentInChildren<FishingLine>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(boatState)
        {
            case "fishing":
                Fishing();
                return;

            case "spinning":
                Spinning();
                return;


            case "fighting": //do nothing, fighting the fish (maybe force lure onto player here)
                return;


            case "sinking":
                Sink();
                return;

            default:
                boatState = "sinking"; //for testing purposes
                return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(this.gameObject.name + " hit " + other.gameObject.name);
        if (other.tag == "Boat" && boatState == "fishing" && !WaitingPostSpin)
        {
            boatState = "spinning";
            WaitingPostSpin = true;
        }
    }


    public void InstantiateBoat(Vector2 oceanClamps, Vector2 spawnClamps)
    {
        //set clamps
        boatClamps = new Vector2(Random.Range(oceanClamps.x, spawnClamps.x),
                                 Random.Range(spawnClamps.y, oceanClamps.y));

        //set speeds
        fishingSpeed = Random.Range(.5f * fishingSpeed, 1.5f * fishingSpeed);
        spinSpeed = Random.Range(.5f * spinSpeed, 1.5f * spinSpeed);


        goingRight = Random.Range(0, 100) < 50 ? true : false;
        if (goingRight)
        {
            goalRot = 180;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            goalRot = 0;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }


    private void Fishing()
    {
        Vector3 dir = goingRight ? Vector3.right : Vector3.left;
        transform.position += dir * fishingSpeed * Time.deltaTime;

        float xPos = transform.position.x;
        if((Mathf.Abs(xPos - boatClamps.x) < 1 && !goingRight) ||
           (Mathf.Abs(xPos - boatClamps.y) < 1 && goingRight))
        {
            boatState = "spinning";
            lure.gameObject.SetActive(false);
        }
    }

    private void Spinning()
    {
        Quaternion l = Quaternion.RotateTowards(transform.rotation,
                                                Quaternion.Euler(0, -goalRot, 0),
                                                spinSpeed * Time.deltaTime);

        Debug.Log("l: " + l);

        transform.rotation = l;

        if (Mathf.Abs(transform.rotation.eulerAngles.y - goalRot) < 1)
        {
            transform.rotation = Quaternion.Euler(0, goalRot, 0);
            goalRot = goalRot == 0 ? 180 : 0;

            goingRight = !goingRight;
            boatState = "fishing";
            StartCoroutine(WaitSpin());
            lure.gameObject.SetActive(true);
        }

    }

    private void Sink()
    {
        //falling down
        fallingSpeed = fallingSpeed + fallAcceleration;
        fallingSpeed = Mathf.Min(fallingSpeed, maxFallSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x,
                                         transform.position.y - fallingSpeed,
                                         transform.position.z);

        //rotating the boat
        if(rotationAmount > 1)
        {
            float currentRot = (rotationAmount * Time.deltaTime)/rotationAcceleration;
            float dir = goingRight ? -1 : 1;

            boatAssets.Rotate(new Vector3(dir * currentRot, 0, dir * currentRot/2));
            rotationAmount -= currentRot;

            //sink lure
            line.midOffset -= fallingSpeed;
        }

        

        //despawn boat
        if(transform.position.y < despawnHeight)
        {
            boatManager.RemoveShip();
            Destroy(this.gameObject);
        }
    }

    public IEnumerator waitLure(){
        
        yield return new WaitForSeconds(3f);
        lure.gameObject.SetActive(true);
    }

    public IEnumerator WaitSpin()
    {
        yield return new WaitForSeconds(15f);
        lure.gameObject.SetActive(true);
        WaitingPostSpin = false;
    }
}
