using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text highScore;

    void Start()
    {
     highScore.text = "Maior Score: "+PlayerPrefs.GetInt("SCORE", 0);   
    }
    public void NewGame(){
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("CampMap");
    }

    public void Quit(){
        Application.Quit();
    }
}
