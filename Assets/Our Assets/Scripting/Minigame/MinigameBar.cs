using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minigame Difficulty Level", menuName = "Minigame Difficulty Level", order = 0)]
[System.Serializable]
public class MinigameBar : ScriptableObject
{
    public float mashingGoal;
    public float[] timeLimits;
    public float[] minTimeLimits;
    public float[] maxTimeLImits;
    
    [Tooltip("The total amount of time the minigame will be active for this level of difficulty")]
    [HideInInspector]
    public float totalTimeLimit;
    public float foodReward;
    public float pointsPerHit;
}
