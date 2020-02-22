using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
        //{{FUNCTION}}LoadScene: Loads scene
        //{{FUNCTION}}GetActiveScene: Gets currently active scene
        //{{FUNCTION}}buildIndex: Returns index of scene in Build Settings
    }
    public void QuitGame()
    {
        Debug.Log("Quit");//To test if quit
        Application.Quit();//{{FUNCTION}}Quit: Quits player application 
    }
}
