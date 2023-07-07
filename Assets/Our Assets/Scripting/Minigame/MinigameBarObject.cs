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
    
    //TODO array of inputs
    
    /*TODO have strength influence
    more strength = easier minigames (input * strength_modifier)
    //*/
    /*TODO points system
    get points for each valid input
    no deduction points on missed input
    //*/

    public float progressBarSpeed; //0 ... 5
    
    MinigameBarObject(float nMashingGoal, float[] nTimeLimits) {
        mashingGoal = nMashingGoal;
        timeLimits = nTimeLimits;
    }
    
    private void Awake() {
        if (minigameBar == null) {
            Debug.Log("minigameBar is not assigned!");
        }
        
        /* TODO
        number of goals be thing - measuring difficulty
            defined
        length of goals be       - "                  "
            min/max length to choose from
        fill number with lengths between min/max

        complexity = thing?
        number of unique buttons pressed
        max of 4 ofc
        # goals == 5 -> 1 duplicate
        no sequential duplicate goals
        //*/
        mashingGoal = minigameBar.mashingGoal;
        timeLimits = minigameBar.timeLimits;
    }
    
    public float GetFoodReward() { return minigameBar.foodReward; }
}
