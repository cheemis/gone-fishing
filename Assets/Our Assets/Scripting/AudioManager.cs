using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip pullingMusic;

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

        _currentMusicSource.volume = MusicVolume;
        //_nextMusicSource.volume = MusicVolume;
    }

    private void Update()
    {
        //UpdateMusic();

        if (Input.GetKey(KeyCode.T))
        {
            PlayMusic(gameMusic);
        }

        if (Input.GetKey(KeyCode.Y))
        {
            PlayMusic(pullingMusic);
        }
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
    
    [SerializeField] private AudioSource _currentMusicSource;
    //[SerializeField] private AudioSource _nextMusicSource;
    
    private float _MusicVolume = 0.8f;
    public float MusicVolume
    {
        get => _MusicVolume;
        set
        {   
            _MusicVolume = value;
            PlayerPrefs.SetFloat("MusicVolume", value);
            _currentMusicSource.volume = MusicVolume;
            //_nextMusicSource.volume = MusicVolume;
            Debug.Log("currentMusicSource.volume: " + _currentMusicSource.volume);
        }
    }

    enum MusicState
    {
        Playing,
        Transitioning,
        FadingOut
    }
    
    private MusicState _musicState = MusicState.Playing;
    

    public void PlayMusic(AudioClip clip)
    {
        float time = _currentMusicSource.time;
        _currentMusicSource.clip = clip;
        _currentMusicSource.Play();
        _currentMusicSource.time = time;
        _currentMusicSource.volume = MusicVolume;
    }

    #endregion
}
