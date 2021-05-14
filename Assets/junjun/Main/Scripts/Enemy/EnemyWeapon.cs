using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] EnemyBase m_enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shield")
        {
            Parry();
        }
    }
    private void Parry()
    {
        if (m_enemy.m_enemyState == EnemyState.Attack)
        {
            m_enemy.m_anim.SetBool("Hit", true);
            m_enemy.m_enemyState = EnemyState.CoolTime;
        }
    }
}
