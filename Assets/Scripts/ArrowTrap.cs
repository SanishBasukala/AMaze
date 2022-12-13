using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;

        fireballs[findFireball()].transform.position = firePoint.position;
        //fireballs[findFireball()].GetComponent<EnemyProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    private int findFireball()
    {
        for(int i = 0; i< fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        //cooldownTimer += Time.delaTime;
        if (cooldownTimer >= attackCooldown)
            Attack();
    }

}

