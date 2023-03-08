using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menuscript : MonoBehaviour
{
    string currentScene;
    //public int levelPassed;
    public Button level2Button, level3Button, level4Button, level5Button;
    private void Start()
    {
        PlayerPrefs.SetInt("levelPassed", 0);
        level2Button.interactable = false;
        level3Button.interactable = false;
        level4Button.interactable = false;
        level5Button.interactable = false;
    }
    private void Update()
    {
        //print(PlayerPrefs.GetInt("levelPassed"));
        switch (PlayerPrefs.GetInt("levelPassed"))
        {
            case 1:
                level2Button.interactable = true;
                level2Button.transform.Find("Locked").gameObject.SetActive(false);
                break;
            case 2:
                level2Button.interactable = true;
                level2Button.transform.Find("Locked").gameObject.SetActive(false);
                level3Button.interactable = true;
                level3Button.transform.Find("Locked").gameObject.SetActive(false);
                break;

            case 3:
                level2Button.interactable = true;
                level2Button.transform.Find("Locked").gameObject.SetActive(false);
                level3Button.interactable = true;
                level3Button.transform.Find("Locked").gameObject.SetActive(false);
                level4Button.interactable = true;
                level4Button.transform.Find("Locked").gameObject.SetActive(false);
                break;
            case 4:
                level2Button.interactable = true;
                level2Button.transform.Find("Locked").gameObject.SetActive(false);
                level3Button.interactable = true;
                level3Button.transform.Find("Locked").gameObject.SetActive(false);
                level4Button.interactable = true;
                level4Button.transform.Find("Locked").gameObject.SetActive(false);
                level5Button.interactable = true;
                level5Button.transform.Find("Locked").gameObject.SetActive(false);
                break;
        }
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
        PlayerPrefs.SetString("currentScene", "Level1");
        SceneManager.LoadScene("Level1");
    }
    public void GetLevel2Scene()
    {
        PlayerPrefs.SetInt("levelPassed", 1);
        PlayerPrefs.SetString("currentScene", "Level2");
        SceneManager.LoadScene("Level2");
    }
    public void GetLevel3Scene()
    {
        PlayerPrefs.SetInt("levelPassed", 2);
        PlayerPrefs.SetString("currentScene", "Level3");
        SceneManager.LoadScene("Level3");
    }
    public void GetLevel4Scene()
    {
        PlayerPrefs.SetInt("levelPassed", 3);
        PlayerPrefs.SetString("currentScene", "Level4");
        SceneManager.LoadScene("Level4");
    }
    public void GetLevel5Scene()
    {
        PlayerPrefs.SetInt("levelPassed", 4);
        PlayerPrefs.SetString("currentScene", "Level5");
        SceneManager.LoadScene("Level5");
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
        currentScene = PlayerPrefs.GetString("currentScene");
        if (currentScene == "Level1")
        {
            GetLevel2Scene();
        }
        if (currentScene == "Level2")
        {
            GetLevel3Scene();
        }
        if (currentScene == "Level3")
        {
            GetLevel4Scene();
        }
        if (currentScene == "Level4")
        {

            GetLevel5Scene();
        }
        if (currentScene == "Level5")
        {
            //Roll end credits

            Debug.Log("Game finished");
        }
    }
    public void Exitgame()
    {
        PopUpDialog.Instance.ShowDialog("Are you sure you want to quit?", () =>
        {
            Debug.Log("bye");//Application.Quit();
        }, () =>
        {
            // Do nothing on close
        });
    }

}
