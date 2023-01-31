using UnityEngine;

public class Manager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(playerHealth);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        playerHealth.health = data.health;
    }
}
