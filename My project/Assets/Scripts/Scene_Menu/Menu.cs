using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {

        print(PlayerPrefs.GetInt("save"));
    }
    private void Update()
    {
        //PlayerPrefs.SetInt("save", 0);
    }
    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("save") == 0)
            return;
        SceneManager.LoadScene("Game");
    }
    public void New_GamePressed()
    {
        //Dialogue.cost1 = 10;
        //Dialogue.cost2 = 10;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Game");
    }

    public void ExitPressed()
    {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}
