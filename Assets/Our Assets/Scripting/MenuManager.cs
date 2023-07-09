using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject CreditsMenu;
    
    [Space]
    
    [SerializeField] private Slider SFXVolumeSlider; 
    [SerializeField] private Slider MusicVolumeSlider;
    
    [Space]
    
    [SerializeField] private GameObject TransitionOutAnimation;
    [SerializeField] private AudioSource MenuSFXSource;
    [SerializeField] private AudioClip TransitionOutAudioClip;
    
    public string gameSceneName = "Main";
    
    private void Start() {
        AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
    }

    #region Button Methods

    public void PlayGame()
    {
        TransitionOutAnimation.SetActive(true);
        
        MenuSFXSource.volume = AudioManager.instance.SFXVolume;
        MenuSFXSource.PlayOneShot(TransitionOutAudioClip);

        Invoke(nameof(LoadGame), 3.8f);
    }
    
    private void LoadGame()
    {
        AudioManager.instance.PlayMusic(AudioManager.instance.gameMusic);
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }
    

    public void OpenSettingsMenu()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        
        SFXVolumeSlider.value = AudioManager.instance.SFXVolume;
        MusicVolumeSlider.value = AudioManager.instance.MusicVolume;
    }
    
    public void OpenCreditsMenu()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }
    
    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    #endregion

    #region Settings Methods
    
    [PublicAPI]
    public void UpdateSFXVolume()
    {
        AudioManager.instance.SFXVolume = SFXVolumeSlider.value;
    }
    
    [PublicAPI]
    public void UpdateMusicVolume()
    {
        AudioManager.instance.MusicVolume = MusicVolumeSlider.value;
    }

    #endregion
}
