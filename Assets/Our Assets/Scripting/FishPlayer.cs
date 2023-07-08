using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishPlayer : MonoBehaviour
{   
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnStrength;
    [SerializeField]
    private Rigidbody2D body;
    
    [SerializeField]
    private Animator animController;

    private Vector2 direction;

    public float strength;

    public static FishPlayer instance;
    
    void Awake(){
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        if(animController == null){
            animController = this.gameObject.GetComponent<Animator>();
        }
        
        if(animController == null){
            Debug.LogError("No Animator on player");
        }
        
    }

    void FixedUpdate(){
        if(body){
            body.AddTorque(-direction.x*turnStrength, ForceMode2D.Force);
            body.AddForce(direction.y*movementSpeed*transform.up, ForceMode2D.Force);
        }
    }

    public void PlayerMove(InputAction.CallbackContext context){
        // Debug.Log("MOVING");
        if(body == null){
            Debug.LogError("No RigidBody2D on player");
        }
        direction = context.ReadValue<Vector2>();
    }

    public void SetStruggle(bool struggle){
        animController.SetBool("Struggling", struggle);
    }

    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log(other);
        if(animController){
            animController.SetTrigger("Eat");
        }
        if(other.tag.Equals("Food")){
            Destroy(other.gameObject);
            strength++;
        }
        if(other.tag.Equals("Lure")){
            SetStruggle(true);
            LureObject game = other.GetComponent<LureObject>();
            FishMinigame.instance.TryRunGame(game, strength);
        }
    }

    public void GameInput(InputAction.CallbackContext context) {
        FishMinigame fishGame = FishMinigame.instance; 
        if(fishGame != null){
            bool correct = fishGame.GameInput(context);
            switch (context.action.name[0]) {
                case 'U':
                    animController.SetTrigger("Up");
                    return;
                case 'L':
                    animController.SetTrigger("Left");
                    return;
                case 'R':
                    animController.SetTrigger("Right");
                    return;
                case 'D':
                    animController.SetTrigger("DOwn");
                    return;
            }
        }   
    }
}