using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public bool isOpen;
    private Animator anim;
    public bool playerInRange;

    public GameObject dialogBox;
    public Image imageHolder;
    public Sprite image;


    private void Start()
    {
        anim = GetComponent<Animator>();
        imageHolder.GetComponent<Image>().sprite = image;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange)
        {

            if (!isOpen)
            {
                GameObject.Find("FinalDoor").GetComponent<FinalDoor>().AddPoint();
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
