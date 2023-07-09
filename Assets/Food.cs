using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    public float power;
    [HideInInspector]
    public Vector3 targetPos;
    void Update(){
        Vector3 direction = targetPos - this.transform.position;
        direction.Normalize();
        float factor = Time.deltaTime * Speed;
        this.transform.Translate(direction.x * factor, direction.y * factor, direction.z * factor, Space.World);
    }
}
