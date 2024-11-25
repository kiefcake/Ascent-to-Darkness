using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject loading;

    public AudioSource buttonSound;

    void Start()
    {
        mainMenu = GameObject.Find("MainMenuCanvas");
        loading = GameObject.Find("LoadingCanvas");
        mainMenu.GetComponent<Canvas>().enabled = true;
        loading.GetComponent<Canvas>().enabled = false;

        // Ensure cursor visibility and lock state are reset
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartButton()
    {
        loading.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
        SceneManager.LoadScene("GameScene");
    }

    public void TutorialButton()
    {
        loading.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
        SceneManager.LoadScene("TutorialAssets");
    }

    public void ExitGameButton()
    {
        buttonSound.Play();
        Application.Quit();
        Debug.Log("App Has Exited");
    }

    void Update()
    {
        //ensure cursor visibility and lock state are reset during gameplay
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}