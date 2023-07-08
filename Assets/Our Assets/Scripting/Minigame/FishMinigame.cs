using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FishMinigame : MonoBehaviour
{
    public static FishMinigame instance;
    private bool running = false;
    private float lastTime;
    private int stageIndex;
    private float inputCount;
    
    //more strength = easier minigames (input * strength_modifier)
    private float strength;
    public float pointsEarned; //xp points
    public FisheringBarGraphic barGraphic;
    
    //* only uncomment for testing!!
    [SerializeField]
    //*/
    private MinigameBarObject currentMinigameBar;
    
    [SerializeField]
    private UnityEvent<MinigameBarObject, float> runGame;
    
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        if (runGame.GetPersistentEventCount() == 0) {
            Debug.Log("no listeners assigned to the runGame event");
        }
        instance = this;
        
    }
    
    //TODO: replace this, either in this script or elsewhere, with the actual way of starting the minigame
    private void OnEnable() {
        /* only uncomment for testing!!
        TryRunGame(currentMinigameBar, 1f);
        //*/
    }

    public void TryRunGame(MinigameBarObject newMinigameBar, float newStrength) {
        if(!newMinigameBar) {
            Debug.LogWarning("No Minigame set");
            return;
        }
        if (!running) runGame.Invoke(newMinigameBar, newStrength);
    }
    
    public void BeginRunningGame(MinigameBarObject newMinigameBar, float newStrength) {
        currentMinigameBar = newMinigameBar;
        barGraphic.ChangeColor(currentMinigameBar.inputs[stageIndex][0]);
        lastTime = 0;
        stageIndex = 0;
        inputCount = 0;
        strength = newStrength;
        running = true;
    }
    
    public bool GameInput(InputAction.CallbackContext context) {
        bool correct = false;
        if (running && context.started){
            correct = context.action.name == currentMinigameBar.inputs[stageIndex];
            if (correct) {
                Debug.Log(context.action.name);
                pointsEarned += currentMinigameBar.pointsPerHit;
                inputCount += strength; // inputCount = inputCount + (1 * strength)
            }
        }
        return correct;
    }
    
    private void Update() {
        if (running) { //only when a minigame is ongoing
            lastTime += Time.deltaTime;
            // Debug.Log(lastTime);
            barGraphic.percentFull = inputCount / currentMinigameBar.mashingGoal;
            barGraphic.timeRemainingPercent = lastTime / currentMinigameBar.totalTimeLimit;
            barGraphic.inputRemainingPercent = (currentMinigameBar.timeLimits[stageIndex] - lastTime)/currentMinigameBar.timeLimits[stageIndex];
            barGraphic.curInput = currentMinigameBar.inputs[stageIndex];
            if (inputCount >= currentMinigameBar.mashingGoal) EndGame(true); //succeeded in completing the minigame
            
            else if (lastTime >= currentMinigameBar.timeLimits[stageIndex]) { //move to next timeLimit/stage
                if (stageIndex + 1 == currentMinigameBar.timeLimits.Count) 
                    EndGame(false); // failed to complete the minigame in time
                else {
                    // lastTime = 0;
                    stageIndex++;
                    currentMinigameBar.timeLimits[stageIndex] += lastTime;
                    barGraphic.ChangeColor(currentMinigameBar.inputs[stageIndex][0]);
                }
            }
        }
    }

	private void EndGame(bool success) {
        Debug.Log(inputCount);
        Debug.Log(pointsEarned);
        running = false;
        if (success) {
            Debug.Log("success");
            //give player points
            //give the worm spawner foodEarned
        }
        else {
            // Debug.Log(currentMinigameBar.totalTimeLimit);
            Debug.Log("failure");
        }
        FishPlayer.instance.SetStruggle(false);
        Destroy(currentMinigameBar.gameObject);
	}
}
