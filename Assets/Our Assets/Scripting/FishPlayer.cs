using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class FishPlayer : MonoBehaviour
{   
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnStrength;
    [SerializeField]
    private Rigidbody2D body;

    private Vector2 direction;

    public float strength;
    
    public float points; //xp
    
    void Awake(){
        // body = this.gameObject.GetComponent<Rigidbody2D>();
        // if(body == null){
        //     Debug.LogError("No RigidBody2D on player");
        // }
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
        if(other.tag.Equals("Food")){
            Destroy(other.gameObject);
            strength++;
        }
    }
}