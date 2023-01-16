using UnityEngine;

public class Skeleton : Enemy
{
    private Rigidbody2D myrigidbody;
    public int damage;
    public PlayerHealth playerHealth;

    private void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
