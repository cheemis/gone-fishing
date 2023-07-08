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
    private LureObject currentLure;
    
    [SerializeField]
    private UnityEvent<LureObject, float> runGame;

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
        TryRunGame(currentLure, 1f);
        //*/
    }

    public void TryRunGame(LureObject newLure, float newStrength) {
        if(!newLure) {
            Debug.LogWarning("No Minigame set");
            return;
        }
        if (!running) BeginRunningGame(newLure, newStrength);
    }
    
    public void BeginRunningGame(LureObject newLure, float newStrength) {
        barGraphic.gameObject.SetActive(true);
        currentLure = newLure;
        currentLure.myBoat.boatState = "fighting";
        barGraphic.ChangeColor(currentLure.inputs[stageIndex][0]);
        lastTime = 0;
        stageIndex = 0;
        inputCount = 0;
        strength = newStrength;
        running = true;
    }
    
    public bool GameInput(InputAction.CallbackContext context) {
        bool correct = false;
        correct = context.action.name == currentLure.inputs[stageIndex];
        if (running && context.started){
            if (correct) {
                //Debug.Log(context.action.name);
                pointsEarned += currentLure.pointsPerHit;
                inputCount += strength; // inputCount = inputCount + (1 * strength)
            }
        }
        return correct;
    }
    
    private void Update() {
        if (running) { //only when a minigame is ongoing
            currentLure.transform.position = FishPlayer.instance.spriteTransform.position - Vector3.back*0.01f;
            lastTime += Time.deltaTime;
            //Debug.Log(lastTime);
            barGraphic.inputRemainingPercent = (currentLure.timeLimits[stageIndex] - lastTime)/ currentLure.timeLimits[stageIndex] ;
            barGraphic.curInput = currentLure.inputs[stageIndex];
            barGraphic.percentFull = inputCount / currentLure.mashingGoal;
            barGraphic.timeRemainingPercent = lastTime / currentLure.totalTimeLimit;
            
            if (inputCount >= currentLure.mashingGoal) EndGame(true); //succeeded in completing the minigame
            
            else if (lastTime >= currentLure.timeLimits[stageIndex]) { //move to next timeLimit/stage
                if (stageIndex + 1 == currentLure.timeLimits.Count) 
                    EndGame(false); // failed to complete the minigame in time
                else {
                    // lastTime = 0;
                    stageIndex++;
                    currentLure.timeLimits[stageIndex] += lastTime;
                    barGraphic.ChangeColor(currentLure.inputs[stageIndex][0]);
                }
            }
        }
    }

	private void EndGame(bool success) {
        //Debug.Log(inputCount);
        //Debug.Log(pointsEarned);
        running = false;
        if (success) {
            Debug.Log("success");
            //give player points
            //give the worm spawner foodEarned
            currentLure.myBoat.boatState = "sinking";
            Instantiate(currentLure.Fisherman, currentLure.myBoat.transform.position+Vector3.down*3f, currentLure.Fisherman.transform.rotation);
            Instantiate(currentLure.WormSpawn, currentLure.transform.position, currentLure.WormSpawn.transform.rotation);
        }
        else {
            //Debug.Log(currentLure.totalTimeLimit);
            // Debug.Log(currentLure.totalTimeLimit);
            Debug.Log("failure");
        }
        FishPlayer.instance.SetStruggle(false);
        currentLure.gameObject.SetActive(false);
        barGraphic.gameObject.SetActive(false);
        //Destroy(currentLure.gameObject);
    }
        
}
