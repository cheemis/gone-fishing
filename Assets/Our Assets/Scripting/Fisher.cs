using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisher : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> screams;
    [SerializeField]
    private AudioSource audioSrc;
    [SerializeField]
    private AudioSource evilFishAudio;
    [SerializeField]
    [Range(0, 1)]
    private float fisherAudioVolume = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    private float evilFishAudioVolume = 0.5f;
    
    [SerializeField]
    private float despawnHeight;
    
    private void Start() {
        if (screams.Count == 0) {
            Debug.LogWarning("No screams assigned to the fisher guy!");
            return;
        }
        if (evilFishAudio == null) {
            Debug.Log("Fisher needs to know about the fish eating it!");
            return;
        }
        audioSrc = gameObject.GetComponent<AudioSource>();
        //pick a scream
        audioSrc.clip = screams[Random.Range(0,screams.Count - 1)];
        //modify the scream?
        audioSrc.volume = fisherAudioVolume * AudioManager.instance.SFXVolume;
        evilFishAudio.volume = evilFishAudioVolume * AudioManager.instance.SFXVolume;
        
        audioSrc.Play();
        evilFishAudio.Play();
    }
    
    private void LateUpdate() {
        if (gameObject.transform.position.y <= despawnHeight) {
            Destroy(gameObject);
        }
    }

    
    private void OnDestroy() {
        audioSrc.Stop();
        evilFishAudio.Stop();
    }
}
