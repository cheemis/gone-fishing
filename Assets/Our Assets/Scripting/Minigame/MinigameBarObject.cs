using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinigameBarObject : MonoBehaviour
{
    [SerializeField]
    private MinigameBar minigameBar;
    public float mashingGoal; //0 ... 5
    public float[] timeLimits; //0 ... 5
    public float totalTimeLimit;
    public string[] inputs;
    
    private static readonly string[] inputNames = {
        "Up", "Left", "Right", "Down"
    };
    
    public float pointsPerHit; //xp

    public float progressBarSpeed; //0 ... 5
    
    private void Awake() {
        if (minigameBar == null) {
            Debug.Log("minigameBar is not assigned!");
        }
        
        pointsPerHit = minigameBar.pointsPerHit;

        mashingGoal = minigameBar.mashingGoal;
        
        totalTimeLimit = 0; 
        foreach(float f in timeLimits) totalTimeLimit += f;
        
        //setup timeLimits
        timeLimits = minigameBar.timeLimits;
        if (timeLimits.Length > 1) 
            for (int i = 0; i < timeLimits.Length; i++) 
                timeLimits[i] = Random.Range(minigameBar.minTimeLimits[i], minigameBar.maxTimeLImits[i]);
        
        //randomly assign inputs
        RandomizeInputs();
        
        //*only uncomment for testing
        FishMinigame.instance.TryRunGame(this, 1f);
        //*/
    }
    
    public void RandomizeInputs() {
        inputs = new string[timeLimits.Length];
        List<int> pickedNums = new List<int>();
        for(int i = 0; i < timeLimits.Length; i++) {
            int num = Random.Range(0, inputNames.Length - 1); //pick a number. all numbers have associated input
            if (pickedNums.ToArray().Length >= 0 && pickedNums.ToArray().Length < inputNames.Length) { //if there are unique numbers not yet picked,
                while (pickedNums.IndexOf(num) != -1) { //increment until a new unique number is found
                    num++;
                    num %= inputNames.Length; //stay in the array
                }
                pickedNums.Insert(0,num); //mark the number as not unique
                inputs[i] = inputNames[num]; //assign the input
            }
            else { //no unique numbers left to be picked
                if (num == pickedNums[0]) { //if the number was picked twice in a row
                    num++;
                    num %= inputNames.Length; //stay in the array
                }
                pickedNums[0] = num; //mark the number as most recently used
                inputs[i] = inputNames[num];
            }
            
        }
    }
    
    public float GetFoodReward() { return minigameBar.foodReward; }
}
