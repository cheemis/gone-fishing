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
    
    public string gameSceneName = "Main";

    #region Button Methods

    public void PlayGame()
    {
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
