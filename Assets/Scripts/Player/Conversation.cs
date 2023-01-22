using UnityEngine;
using UnityEngine.UI;


public class Conversation : MonoBehaviour
{
    //For conversation
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public string dialogActive;
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(true);
        dialogActive = "true";
        dialogText.text = dialog;
    }

    //For conversation 
    public void ForConversation()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                dialogActive = "false";
            }
        }
    }
}
