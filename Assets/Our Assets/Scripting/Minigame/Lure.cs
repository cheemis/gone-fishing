using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minigame Difficulty Level", menuName = "Minigame Difficulty Level", order = 0)]
[System.Serializable]
public class Lure : ScriptableObject
{
    public float mashingGoal;
    public List<float> timeLimits;
    // public List<float> minTimeLimits;
    // public List<float> maxTimeLImits;
    
    public float foodReward;
    public float pointsPerHit;
}
