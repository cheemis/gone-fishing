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
    private float strength;
    public float pointsEarned;
    
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
    
    private void Start() {
        //* only uncomment for testing!!
        TryRunGame(currentMinigameBar, 1f);
        //*/
    }
    
    private void Update() {
        if (running) { //only when a minigame is ongoing
            lastTime += Time.deltaTime;
            if (inputCount >= currentMinigameBar.mashingGoal) EndGame(true); //succeeded in completing the minigame
            
            else if (lastTime >= currentMinigameBar.timeLimits[stageIndex]) {
                if (stageIndex + 1 == currentMinigameBar.timeLimits.Length) { // failed to complete the minigame in time
                    EndGame(false);
                }
                else {
                    lastTime = 0;
                    stageIndex++;
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
            Debug.Log("failure");
        }
	}

    public void TryRunGame(MinigameBarObject newMinigameBar, float newStrength) {
        if (!running) runGame.Invoke(newMinigameBar, 1f);
    }
    
    public void BeginRunningGame(MinigameBarObject newMinigameBar, float newStrength) {
        lastTime = 0;
        stageIndex = 0;
        inputCount = 0;
        strength = newStrength;
        running = true;
        currentMinigameBar = newMinigameBar;
    }
    
    public void InputUp(InputAction.CallbackContext context) {
        if (running && context.started)
            if (context.action.name == currentMinigameBar.inputs[stageIndex]) {
                Debug.Log(context.action.name);
                pointsEarned += currentMinigameBar.pointsPerHit;
                inputCount += strength;
            }
    }
    
    public void InputLeft(InputAction.CallbackContext context) {
        if (running && context.started)
            if (context.action.name == currentMinigameBar.inputs[stageIndex]) {
                Debug.Log(context.action.name);
                pointsEarned += currentMinigameBar.pointsPerHit;
                inputCount += strength;
            }
    }
    
    public void InputRight(InputAction.CallbackContext context) {
        if (running && context.started)
            if (context.action.name == currentMinigameBar.inputs[stageIndex]) {
                Debug.Log(context.action.name);
                pointsEarned += currentMinigameBar.pointsPerHit;
                inputCount += strength;
            }
    }
    
    public void InputDown(InputAction.CallbackContext context) {
        if (running && context.started)
            if (context.action.name == currentMinigameBar.inputs[stageIndex]) {
                Debug.Log(context.action.name);
                pointsEarned += currentMinigameBar.pointsPerHit;
                inputCount += strength;
            }
    }
}
