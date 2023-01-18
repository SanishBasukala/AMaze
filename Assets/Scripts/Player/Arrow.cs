using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody2D;

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidbody2D.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction); // quaternion for finding direction something is facing
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

    }
}
