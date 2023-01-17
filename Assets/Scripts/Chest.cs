using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public bool isOpen;
    private Animator anim;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;
    public bool chestDialogActive = false;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange)
        {

            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestIsOpen();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            playerInRange = false;
        }
    }

    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = "Description";
        isOpen = true;
        chestDialogActive = true;

        anim.SetBool("opened", true);
    }
    public void ChestIsOpen()
    {
        dialogBox.SetActive(false);
        chestDialogActive = false;
    }
}
