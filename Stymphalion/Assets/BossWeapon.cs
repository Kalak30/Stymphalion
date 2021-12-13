using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public Transform m_attackPoint;
    public LayerMask m_enemyLayers;
    public int m_attackDamage = 100;

    public float m_attackRange = 4;
    public LayerMask attackMask;

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(m_attackPoint.position, m_attackRange, m_enemyLayers);

        //Damage enemies in range
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Player>().TakeDamage(m_attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (m_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(m_attackPoint.position, m_attackRange);
    }
}