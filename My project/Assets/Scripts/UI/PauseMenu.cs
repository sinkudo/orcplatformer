using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject deathscreen;
    public Save save;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (isPaused)
                Resume();
            else
                Pause();
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void QuitGame()
    {
        GetComponent<Save>().SaveGame();
        SceneManager.LoadScene("Menu");
    }
    public void QuitGameWithoutSave()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadAfterDeath()
    {
        SceneManager.LoadScene("Menu");
        SceneManager.LoadScene("Game");
    }
}
