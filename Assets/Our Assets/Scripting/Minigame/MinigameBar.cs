using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minigame Difficulty Level", menuName = "Minigame Difficulty Level", order = 0)]
[System.Serializable]
public class MinigameBar : ScriptableObject
{
    public float mashingGoal;
    public float[] timeLimits;
    public float foodReward;
}
