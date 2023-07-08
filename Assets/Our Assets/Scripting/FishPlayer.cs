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

    [SerializeField]
    private PlayerInput inputs;

    public Transform spriteTransform;


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
        if(struggle) {
            inputs.currentActionMap = inputs.actions.FindActionMap("Fishing Minigame");
            body.velocity = Vector2.zero;   
        }
        else {
            inputs.currentActionMap = inputs.actions.FindActionMap("Player");
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log(other);
        if(animController){
            animController.SetTrigger("Eat");
        }
        if(other.tag.Equals("Food")){
            Destroy(other.gameObject);
            strength+=other.GetComponent<Food>().power;
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
            animController.SetBool("Correct", correct);
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
                    animController.SetTrigger("Down");
                    return;
            }
        }   
    }
}