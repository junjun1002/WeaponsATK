using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    /// <summary>�G�̃I�u�W�F�N�g�i�e�I�u�W�F�N�g�j</summary>
    [SerializeField] EnemyBase m_enemy;
    /// <summary>���ƏՓ˂������ɏo��G�t�F�N�g</summary>
    [SerializeField] ParticleSystem m_hitEffect;

    BoxCollider col;

    /// <summary>
    /// �U�������Ŏ󂯂�ꂽ�Ƃ��ɌĂ΂��
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
    /// Player�ɏ��Œe���ꂽ�Ƃ��ɌĂ΂��֐�
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
