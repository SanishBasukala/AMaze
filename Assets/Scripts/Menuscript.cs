using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuscript : MonoBehaviour
{
    Scene currentScene;

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    public void GetMainScene()
    {
        SceneManager.LoadScene("BackgroundScene");
    }
    public void GetLevelScene()
    {
        SceneManager.LoadScene("LevelScene");
    }
    public void GetLevel1Scene()
    {
        SceneManager.LoadScene("Level1");
    }
    public void GetCreateScene()
    {
        SceneManager.LoadScene("Create");
    }
    public void GetFinalDoorScene()
    {
        SceneManager.LoadScene("FinalDoor");
    }
    public void ChangeScene()
    {
        if (currentScene == SceneManager.GetSceneByName("Level1"))
        {
            //SceneManager.LoadScene("Level2");
        }
    }
    public void Exitgame()
    {
        Application.Quit();
    }

}
