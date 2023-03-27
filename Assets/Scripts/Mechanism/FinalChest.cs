using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalChest : MonoBehaviour
{
    public GameObject dialogBox;

    public bool EndDialogue = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            EndDialogue = true;
            Time.timeScale = 0f;
            AudioListener.pause = true;
            dialogBox.SetActive(true);
        }
    }

    public void EndCredits()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("EndCredits");
    }
}
