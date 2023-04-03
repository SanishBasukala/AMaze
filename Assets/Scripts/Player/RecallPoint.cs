using UnityEngine;

public class RecallPoint : MonoBehaviour
{
    public bool playerInRange;
    public GameObject hoverText;
    private void Update()
    {
        if (playerInRange)
        {
            Menuscript menuscript = new();
            menuscript.GetCreateScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerInRange = false;
        }
    }
}

