using System.Collections;
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
                StartCoroutine(OpenChest());
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

    private IEnumerator OpenChest()
    {
        dialogBox.SetActive(true);

        dialogText.text = "Description";
        isOpen = true;
        anim.SetBool("opened", true);
        yield return new WaitForSeconds(2);
        dialogBox.SetActive(false);
    }

    public void ChestIsOpen()
    {
        dialogBox.SetActive(false);
    }
}
