using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public string mainMenu;
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

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
