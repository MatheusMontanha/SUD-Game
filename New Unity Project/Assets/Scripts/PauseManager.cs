using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isPaused;
    public GameObject pausePanel;
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("escape");
            TooglePauseMenu();
        }
    }

    public void Resume()
    {
        TooglePauseMenu();
    }

    private void TooglePauseMenu()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1f;
    }
}
