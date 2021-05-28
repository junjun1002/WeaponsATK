using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    /// <summary>敵のオブジェクト（親オブジェクト）</summary>
    [SerializeField] EnemyBase m_enemy;
    /// <summary>盾と衝突した時に出るエフェクト</summary>
    [SerializeField] ParticleSystem m_hitEffect;
    /// <summary>Playerのオブジェクト</summary>
    [SerializeField] VRPlayerController m_player;

    /// <summary>
    /// 攻撃を盾で受けられたときに呼ばれる
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.tag == "Shield")
        {
            GetComponent<BoxCollider>().enabled = false;
            m_hitEffect.gameObject.SetActive(false);
            Parry();
            m_hitEffect.gameObject.SetActive(true);
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("くらえ");
            m_enemy.m_atkPoint = Random.Range(0.05f, 0.08f);
            Debug.Log(m_enemy.m_atkPoint);
            UIManager.Instance.DecreasesHPUI(m_enemy.m_atkPoint);
            m_player.m_playerHp -= m_enemy.m_atkPoint;
        }
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
