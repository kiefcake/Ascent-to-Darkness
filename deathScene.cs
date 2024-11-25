using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathScene : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject loading;

    public AudioSource buttonSound;

    void Start()
    {
        //set references to UI canvases
        mainMenu = GameObject.Find("DeathSceneCanvas");
        loading = GameObject.Find("LoadingCanvas");

        //enable DeathSceneCanvas and disable LoadingCanvas initially
        mainMenu.GetComponent<Canvas>().enabled = true;
        loading.GetComponent<Canvas>().enabled = false;
    }

    //handle button click to start Scene1
    public void StartButton()
    {
        //enable LoadingCanvas and disable DeathSceneCanvas
        loading.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;

        buttonSound.Play();

        //load Scene1
        SceneManager.LoadScene("Scene1");
    }

    public void TutorialButton()
    {

        loading.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;

        buttonSound.Play();


        SceneManager.LoadScene("Scene3");
    }


    public void ExitGameButton()
    {
        
        buttonSound.Play();

        //exit the application
        Application.Quit();

        //log message
        Debug.Log("App Has Exited");
    }

    void Update()
    {

    }
}