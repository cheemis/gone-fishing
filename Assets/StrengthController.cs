using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthController : MonoBehaviour
{

    public float strength;
    [SerializeField]
    private TMPro.TMP_Text main;
    [SerializeField]
    private TMPro.TMP_Text shadow;

    [SerializeField]
    private Animator animController;

    public bool ping = false;


    // Update is called once per frame
    void Update()
    {
        if(ping){
            Ding();
        }
        string result = Mathf.CeilToInt(strength*100).ToString()+'%';
        
        main.text = result;
        shadow.text = result;

    }

    void Ding(){
        animController.SetTrigger("Ding");
        ping = false;
    }
}
