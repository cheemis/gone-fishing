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
    private float countTime;
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

    public StrengthController strengthUI;

    private float phaseChange = 0;
    private float startPhase = 0;

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
        stageIndex = 0;
        barGraphic.ChangeColor(currentLure.inputs[stageIndex][0]);
        countTime = 0;
        inputCount = 0;
        startPhase = 0;
        phaseChange = currentLure.timeLimits[0];
        // strength = newStrength;
        currentLure.gameObject.GetComponent<AudioSource>().Play();
        AudioManager.instance.PlayMusic(AudioManager.instance.pullingMusic);
        running = true;
    }
    
    public bool GameInput(InputAction.CallbackContext context) {
        bool correct = false;
        if (running){
            correct = context.action.name == currentLure.inputs[stageIndex];
            if (correct && context.started) {
                //Debug.Log(context.action.name);
                pointsEarned += currentLure.pointsPerHit;
                inputCount += strength; // inputCount = inputCount + (1 * strength)
                barGraphic.success = true;
            }
        }
        return correct;
    }
    
    private void Update() {
        strength = FishPlayer.instance.strength;
        barGraphic.gameObject.SetActive(running);
        if(running){
            currentLure.transform.position = FishPlayer.instance.mouthPos.position;
            countTime += Time.deltaTime;
            
            if (inputCount >= currentLure.mashingGoal) EndGame(true);
            else if(countTime >= phaseChange){
                if(countTime >= currentLure.totalTimeLimit) EndGame(false);
                else{
                    stageIndex++;
                    phaseChange = countTime+currentLure.timeLimits[stageIndex];
                    startPhase = countTime;
                    barGraphic.ChangeColor(currentLure.inputs[stageIndex][0]);
                }
            }
            barGraphic.inputRemainingPercent = (countTime - startPhase) / (phaseChange - startPhase);
            barGraphic.curInput = currentLure.inputs[stageIndex];
            barGraphic.percentFull = inputCount / currentLure.mashingGoal;
            barGraphic.timeRemainingPercent = countTime / currentLure.totalTimeLimit;
        }
        strengthUI.strength = strength;
        
    }

	private void EndGame(bool success) {
        //Debug.Log(inputCount);
        //Debug.Log(pointsEarned);
        running = false;
        if (success) {
            // Debug.Log("success");
            //give player points
            //give the worm spawner foodEarned
            currentLure.gameObject.GetComponent<AudioSource>().Stop();
            currentLure.myBoat.audioSource.Stop();
            currentLure.myBoat.audioSource.clip = currentLure.myBoat.boatSink;
            currentLure.myBoat.audioSource.loop = false;
            currentLure.myBoat.audioSource.Play();
            currentLure.myBoat.boatState = "sinking";
            Instantiate(currentLure.Fisherman, currentLure.myBoat.transform.position+Vector3.down*3f, currentLure.Fisherman.transform.rotation);
            Instantiate(currentLure.WormSpawn, currentLure.transform.position, currentLure.WormSpawn.transform.rotation);
        }
        else {
            //Debug.Log(currentLure.totalTimeLimit);
            // Debug.Log(currentLure.totalTimeLimit);
            // Debug.Log("failure");
            currentLure.myBoat.boatState = "fishing";
            StartCoroutine(currentLure.myBoat.waitLure());
        }
        AudioManager.instance.PlayMusic(AudioManager.instance.gameMusic);
        FishPlayer.instance.SetStruggle(false);
        currentLure.gameObject.SetActive(false);
        barGraphic.gameObject.SetActive(false);
        //Destroy(currentLure.gameObject);
    }
        
}
