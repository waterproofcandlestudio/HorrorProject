using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool optionsMenuOn = false;

    public GameObject pauseMenuUI;

    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused == true && optionsMenuOn == false)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        optionsMenuOn = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Player.GetComponent<FPSCamera>().enabled = true;

        PlayerUIManager.instance.PlayUI("enterMenu");
        PlayerSFXManager.instance.Stop();
        //AudioListener.volume = 1;
    }
    public void KeepPause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Player.GetComponent<FPSCamera>().enabled = false;

        //AudioListener.volume = 0;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        optionsMenuOn = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Player.GetComponent<FPSCamera>().enabled = false;

        PlayerUIManager.instance.PlayUI("enterMenu");
        PlayerSFXManager.instance.PlaySFX("heartBeat2");
        PlayerSFXManager.instance.Loop();

        //AudioListener.volume = 0;
    }
    public void LoadOptions()
    {
        optionsMenuOn = true;
    }
    public void ApplyButton()
    {
        optionsMenuOn = false;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1;
        Player.GetComponent<FPSCamera>().enabled = true;

        SceneManager.LoadScene("Menu");
    }
    public void QuitMenu()
    {
        Application.Quit();
    }
}
