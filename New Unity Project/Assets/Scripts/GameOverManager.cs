using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverManager : MonoBehaviour
{
    private bool p1dead;
    private bool p2dead;
    private bool gameOver;
    private bool startedButtonCoroutine;
    public GameObject gameOverPanel;
    public GameObject quitButton;
    private CanvasGroup canvasGroup;
    private float fadeInDelay = 2f;

    public FloatValue score;

    public Text scoreText;
    public Text highScoreText;
    void Start()
    {
        canvasGroup = gameOverPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

    }

    IEnumerator ShowExitButton()
    {
        yield return new WaitForSecondsRealtime(2f);
        quitButton.SetActive(true);

    }
    private void Update()
    {
        Debug.Log(score.value);
        if (gameOver && canvasGroup.alpha < 1f)

        {
            canvasGroup.alpha += Time.unscaledDeltaTime / fadeInDelay;
        }
        if (canvasGroup.alpha >= 1f)
        {
            StartCoroutine(ShowExitButton());
        }

    }


    public void P1Died()
    {
        p1dead = true;
        Debug.Log("player1 dead signal received");
        showGameOverScreen();
    }
    public void P2Died()
    {
        Debug.Log("player2   dead signal received");
        p2dead = true;
        showGameOverScreen();
    }

    private void showGameOverScreen()
    {
        Debug.Log("is game over?");
        if (p1dead && p2dead)
        {
            Debug.Log("GAME OVER");
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            gameOver = true;
            AudioSource[] ass = FindObjectsOfType<AudioSource>() as AudioSource[];
            for (int i = 0; i < ass.Length; i++)
            {
                ass[i].Stop();
            }
            scoreText.text = "Seu Score: " + score.value;

            if (score.value > PlayerPrefs.GetInt("SCORE", 0))
            {
                PlayerPrefs.SetInt("SCORE", (int)score.value);
            }
            highScoreText.text = "Maior Score: " + PlayerPrefs.GetInt("SCORE", 0);
            GetComponent<AudioSource>().Play();
        }
    }
}
