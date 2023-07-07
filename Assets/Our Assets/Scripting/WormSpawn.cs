using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSpawn : MonoBehaviour
{
    [SerializeField]
    private int count;
    [SerializeField]
    private GameObject Food;

    [SerializeField]
    private float maxDistance;
    void Awake(){
        for(int i = 0; i < count; i++) {
            Vector3 randomPos = (transform.position + new Vector3(Random.Range(-maxDistance/2f,maxDistance/2f), Random.Range(-maxDistance/2f,maxDistance/2f), 0));
            Quaternion randomRotation = Quaternion.Euler(0,0, Random.Range(0,360));
            Instantiate(Food, randomPos, randomRotation);
        }
    }
}
