[System.Serializable]
public class PlayerData
{
    public float health;

    public PlayerData(PlayerHealth playerHealth)
    {
        health = playerHealth.health;
    }
}
