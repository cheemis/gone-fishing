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

    [SerializeField]
    private float dashSpeed;

    [SerializeField]
    private float dashCooldown;

    [SerializeField]
    private SpriteRenderer sprite;

    public Transform spriteTransform;
    
    [SerializeField]
    private AudioSource chompAudioSource;

    [SerializeField] 
    private AudioClip chompAudioClip;


    private Vector2 direction;
    
    private bool push;

    private bool allowPush;
    
    public float strength;

    public static FishPlayer instance;

    public Transform mouthPos;
    
    
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
        allowPush = true;
        
    }

    void FixedUpdate(){
        if(body){
            body.AddTorque(-direction.x*turnStrength, ForceMode2D.Force);
            body.AddForce(direction.y*movementSpeed*transform.up, ForceMode2D.Force);
            if(allowPush && Mathf.Abs(direction.y) > 0){ //push &&
                body.AddForce(0.5f*direction.y*dashSpeed*transform.up, ForceMode2D.Impulse);
                StartCoroutine(waitPush());
            }
        }
    }
    

    private IEnumerator waitPush(){
        allowPush = false;
        animController.SetTrigger("Swim");
        yield return new WaitForSeconds(dashCooldown);
        allowPush = true;
    }
    public void PlayerMove(InputAction.CallbackContext context){
        direction = context.ReadValue<Vector2>();
    }

    public void PlayerDash(InputAction.CallbackContext context){
        push = context.ReadValueAsButton();
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
            body.AddForce(dashSpeed*transform.up, ForceMode2D.Impulse);
            FishMinigame.instance.strengthUI.ping = true;
            chompAudioSource.pitch = Random.Range(0.9f, 1.1f);
            chompAudioSource.volume = AudioManager.instance.SFXVolume;
            chompAudioSource.PlayOneShot(chompAudioClip);
        }
        if(other.tag.Equals("Lure")){
            SetStruggle(true);
            LureObject game = other.GetComponent<LureObject>();
            FishMinigame.instance.TryRunGame(game, strength);
        }
    }

    private void ResetAllTriggers(){
        foreach (var param in animController.parameters){
            if (param.type == AnimatorControllerParameterType.Trigger){
                animController.ResetTrigger(param.name);
            }
        }
    } 

    public void GameInput(InputAction.CallbackContext context) {
        FishMinigame fishGame = FishMinigame.instance; 
        if(fishGame != null){
            bool correct = fishGame.GameInput(context);
            animController.SetBool("Correct", correct);
            ResetAllTriggers();
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