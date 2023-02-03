using UnityEngine;

public class Saw : MonoBehaviour
{
    //private Rigidbody2D myrigidbody;
    public int baseAttack;
    public PlayerHealth playerHealth;

    //private void Start()
    //{
    //    myrigidbody = GetComponent<Rigidbody2D>();
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(baseAttack);
        }
    }
}
