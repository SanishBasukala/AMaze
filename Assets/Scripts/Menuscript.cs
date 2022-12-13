using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuscript : MonoBehaviour
{
    public void getMainScene()
    {
        SceneManager.LoadScene("BackgroundScene");
    }
    public void getCreateScene()
    {
        SceneManager.LoadScene("CreateMapScene");
    }
    public void exitgame()
    {
        Application.Quit();
    }

}
