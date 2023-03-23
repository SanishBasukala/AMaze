using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;

    private void Start()
    {
        isPaused = false;
    }
    private void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
            ChangePause();
        }
    }
    public void ChangePause()
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

    public void GetLevelScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelScene");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Menuscript menuscript = new();
        SceneManager.LoadScene(menuscript.GetCurrentScene());
    }
}
