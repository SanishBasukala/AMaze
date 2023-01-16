using UnityEngine;

public class Knockback : MonoBehaviour
{
    //[SerializeField]
    //private Rigidbody2D rigidbody2d;
    //[SerializeField]
    //private float strength = 16, delay = 0.15f;
    //public UnityEvent OnBegin, OnDone;

    //public void PlayKnockBack(GameObject sender)
    //{
    //    StopAllCoroutines();
    //    OnBegin?.Invoke(); //to begin other function when we start knockback
    //    Vector2 direction = (transform.position - sender.transform.position).normalized;
    //    rigidbody2d.AddForce(direction * strength, ForceMode2D.Impulse);
    //    StartCoroutine(Reset());
    //}
    //private IEnumerator Reset()
    //{
    //    yield return new WaitForSeconds(delay);
    //    rigidbody2d.velocity = Vector3.zero;
    //    OnDone?.Invoke();
    //}

    public float thrust;
    public float knockTime;
    public float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if (other.gameObject.CompareTag("Enemy"))
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {

                    if (other.GetComponent<Player>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<Player>().currentState = PlayerState.stagger;
                        other.GetComponent<Player>().Knock(knockTime);
                    }

                }
            }
        }
    }
}
