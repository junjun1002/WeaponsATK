using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    /// <summary>敵のオブジェクト（親オブジェクト）</summary>
    [SerializeField] EnemyBase m_enemy;
    /// <summary>盾と衝突した時に出るエフェクト</summary>
    [SerializeField] ParticleSystem m_hitEffect;

    BoxCollider col;

    /// <summary>
    /// 攻撃を盾で受けられたときに呼ばれる
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Shield")
        {
            //this.GetComponent<BoxCollider>().enabled = false;
            m_hitEffect.gameObject.SetActive(false);
            Parry();
            m_hitEffect.gameObject.SetActive(true);
        }

        m_enemy.OnTriggerEnter(other);
    }

    /// <summary>
    /// Playerに盾で弾かれたときに呼ばれる関数
    /// </summary>
    private void Parry()
    {
        if (m_enemy.m_enemyState == EnemyStateType.Attack)
        {
            m_enemy.m_anim.SetBool("Hit", true);
            m_enemy.m_enemyState = EnemyStateType.CoolTime;
        }
    }

    
}
