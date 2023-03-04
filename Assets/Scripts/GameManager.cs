using UnityEngine;

public class GameManager : MonoBehaviour
{


    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public string currentScene;

    private void Awake()
    {
        instance = this;
        if (PlayerPrefs.HasKey("currentScene"))
        {

            currentScene = PlayerPrefs.GetString("currentScene");
        }
    }
}
