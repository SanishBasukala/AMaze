using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    private int pointsToWin = 6;
    private int currentPoints;
    private bool playerInRange;
    void Update()
    {
        if (currentPoints < pointsToWin && playerInRange)
        {
            // Do nothing when not enough points
        }
        else if (currentPoints == pointsToWin && playerInRange)
        {
            Menuscript menuscript = new();
            menuscript.GetFinalDoorScene();
        }
    }
    public void AddPoint()
    {
        currentPoints++;
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
