using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnLocaion;
    public Quaternion spawnRotation;
    public float spawnTime = 0.5f;
    public float timeSinceSpawned = 0f;
    private void Update()
    {
        timeSinceSpawned += Time.deltaTime;

        if (timeSinceSpawned >= spawnTime)
        {
            Instantiate(projectile, spawnLocaion.position, spawnRotation);
            timeSinceSpawned = 0;
        }
    }
}
