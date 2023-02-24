using System.Collections;
using UnityEngine;


public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public float moveSpeed;

    public FloatValue maxhealth;
    public float health;
    public int baseAttack;
    public GameObject deathEffect;
    public AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip hurtClip;
    public AudioClip deathClip;
    private void Awake()
    {
        health = maxhealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        audioSource.PlayOneShot(hurtClip);
        if (health <= 0)
        {
            print("hi");
            audioSource.PlayOneShot(deathClip);
            DeathEffect();
            this.gameObject.SetActive(false);
            print("hi2");
        }
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        StartCoroutine(DamageIndicator());
        TakeDamage(damage);
    }
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    IEnumerator DamageIndicator()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(.08f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

}
