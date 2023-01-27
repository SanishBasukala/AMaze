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

    //public int maxHealth;
    //public int currentHealth;

    //private void Start()
    //{
    //    currentHealth = maxHealth;
    //}

    //private void Update()
    //{
    //    if (currentHealth <= 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //public void HurtEnemy(int damageAmount)
    //{
    //    currentHealth -= damageAmount;
    //}

    //public void SetMaxHealth()
    //{
    //    currentHealth = maxHealth;
    //}
    public FloatValue maxhealth;
    public float health;
    public int baseAttack;
    public GameObject deathEffect;

    private void Awake()
    {
        health = maxhealth.initialValue;
    }
    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathEffect();
            this.gameObject.SetActive(false);
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


}
