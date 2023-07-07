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
        
        //* only uncomment for testing!!
        TryRunGame(currentMinigameBar, 1f);
        //*/
    }
    
    private void Update() {
        if (running) {
            lastTime += Time.deltaTime;
            if (inputCount >= currentMinigameBar.mashingGoal) {
                
            }
            else if (lastTime >= currentMinigameBar.timeLimits[stageIndex]) NextTimeLimit();
        }
    }
    
    private void NextTimeLimit() {
        if (stageIndex == currentMinigameBar.timeLimits.Length) {
            
        }
    }
    
    public void TryRunGame(MinigameBarObject newMinigameBar, float newStrength) {
        if (!running) {
            runGame.Invoke(newMinigameBar, 1f);
        }
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
        if (running && context.started) {
            if (context.action.name == currentMinigameBar.inputs[stageIndex]) {
                
            }
        }
    }
    
    public void InputLeft(InputAction.CallbackContext context) {
        if (running && context.started) {
            
        }
    }
    
    public void InputRight(InputAction.CallbackContext context) {
        if (running && context.started) {
            
        }
    }
    
    public void InputDown(InputAction.CallbackContext context) {
        if (running && context.started) {
            
        }
    }
}
