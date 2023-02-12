using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string Game;
    public GameObject ScreenSettings;
    public GameObject TitleMusic;
    public GameObject RickRoll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame() 
    {
        SceneManager.LoadScene(Game);
    }

    public void OpenSettings()
    {
        ScreenSettings.SetActive(true);
    }

    public void QuitSettings()
    {
        ScreenSettings.SetActive(false);
    }
    
    public void Close()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }


    public void AWESOME()
    {
        TitleMusic.SetActive(false);
        RickRoll.SetActive(true);

        Debug.Log("Never Gonna Give You Up");
    }
}
