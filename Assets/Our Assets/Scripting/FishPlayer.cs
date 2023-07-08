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

    void Awake(){
        
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

    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log(other);
        if(animController){
            animController.SetTrigger("Eat");
        }
        if(other.tag.Equals("Food")){
            Destroy(other.gameObject);
            strength++;
        }
    }


}