using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LureObject : MonoBehaviour
{
    [SerializeField]
    private Lure lure;
    public float mashingGoal; //0 ... 5
    public List<float> timeLimits; //0 ... 5
    public float totalTimeLimit;
    public List<string> inputs;

    public GameObject Fisherman;
    public GameObject WormSpawn;
    
    private static readonly string[] inputNames = {
        "Up", "Left", "Right", "Down"
    };
    
    public float pointsPerHit; //xp

    public float progressBarSpeed; //0 ... 5

    public FishingBoat myBoat;
    
    private void Awake() {
        if (lure == null) {
            Debug.Log("lure is not assigned!");
        }
        
        gameObject.GetComponent<AudioSource>().volume *= AudioManager.instance.SFXVolume;
        
        Fisherman = lure.Fishermen[Random.Range(0, lure.Fishermen.Count-1)];
        WormSpawn = lure.WormSpawn;
        pointsPerHit = lure.pointsPerHit;

        mashingGoal = lure.mashingGoal;
        
        //setup timeLimits
        timeLimits = new List<float>(lure.timeLimits);
        // if (timeLimits.Count > 1) 
        //     for (int i = 0; i < timeLimits.Count; i++) 
        //         timeLimits[i] = Random.Range(lure.minTimeLimits[i], lure.maxTimeLImits[i]);
        
        totalTimeLimit = 0; 
        foreach(float f in timeLimits) {totalTimeLimit += f;}
        //randomly assign inputs
        RandomizeInputs();
        
        //*only uncomment for testing
        //FishMinigame.instance.TryRunGame(this, 1f);
        //*/
    }


    public void RandomizeInputs() {
        inputs = new List<string>();
        List<int> pickedNums = new List<int>();
        for(int i = 0; i < timeLimits.Count; i++) {
            int num = Random.Range(0, inputNames.Length - 1); //pick a number. all numbers have associated input
            if (pickedNums.ToArray().Length >= 0 && pickedNums.ToArray().Length < inputNames.Length) { //if there are unique numbers not yet picked,
                while (pickedNums.IndexOf(num) != -1) { //increment until a new unique number is found
                    num++;
                    num %= inputNames.Length; //stay in the array
                }
                pickedNums.Insert(0,num); //mark the number as not unique
                inputs.Add(inputNames[num]); //assign the input
            }
            else { //no unique numbers left to be picked
                if (num == pickedNums[0]) { //if the number was picked twice in a row
                    num++;
                    num %= inputNames.Length; //stay in the array
                }
                pickedNums[0] = num; //mark the number as most recently used
                inputs.Add(inputNames[num]);
            }
            
        }
    }
    
    public float GetFoodReward() { return lure.foodReward; }
}
