using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.8f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
    }

    private void Update()
    {
        UpdateMusic();
    }

    #region SFXs

    private float _SFXVolume = 0.8f;
    public float SFXVolume
    {
        get => _SFXVolume;
        set
        {
            _SFXVolume = value;
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
    }

    public void PlaySFX(AudioClip clip, Vector3? pos = null)
    {
        if (pos != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos.Value);
        }
        else
        {
            AudioSource.PlayClipAtPoint(clip, Camera.current.transform.position);
        }
    }
    
    #endregion

    #region Music
    
    private float _MusicVolume = 0.8f;
    public float MusicVolume
    {
        get => _MusicVolume;
        set
        {
            _MusicVolume = value;
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
    }

    private AudioSource _currentMusicSource;
    private AudioSource _nextMusicSource;

    private void UpdateMusic()
    {
        
    }

    #endregion
}
