using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishMinigame : MonoBehaviour
{
    public static FishMinigame instance;
    private bool running = false;
    private MinigameBarObject currentMinigameBar;
    
    [SerializeField]
    private UnityEvent<MinigameBarObject> runGame;
    
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
    
    public void TryRunGame(MinigameBarObject newMinigameBar) {
        if (!running) runGame.Invoke(newMinigameBar);
    }
}
