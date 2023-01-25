using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuscript : MonoBehaviour
{
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
        SceneManager.LoadScene("CreateWithTileMaps");
    }
    public void Exitgame()
    {
        Application.Quit();
    }

}
