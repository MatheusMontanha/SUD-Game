using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame(){
        SceneManager.LoadScene("CampMap");
    }

    public void Quit(){
        Application.Quit();
    }
}
