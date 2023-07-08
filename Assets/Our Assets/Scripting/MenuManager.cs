using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject CreditsMenu;
    
    public string gameSceneName = "Main";
    
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }

    public void OpenSettingsMenu()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        SettingsMenu.SetActive(true);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
