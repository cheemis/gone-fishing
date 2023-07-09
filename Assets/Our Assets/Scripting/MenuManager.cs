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
    
    [SerializeField]
    private AudioSource mainMenuMusic;
    
    public string gameSceneName = "Main";
    
    private void Start() {
        if (mainMenuMusic == null) {
            Debug.Log("no main menu music source assigned!");
        }
        
        mainMenuMusic.PlayDelayed(0.5f);
    }

    #region Button Methods

    public void PlayGame()
    {
        mainMenuMusic.Stop();
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
