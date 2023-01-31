using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * transform.right;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1);
            Destroy(this.gameObject);
        }
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
