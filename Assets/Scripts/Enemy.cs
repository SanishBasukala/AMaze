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

    private void Awake()
    {
        health = maxhealth.initialValue;
    }
    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
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
