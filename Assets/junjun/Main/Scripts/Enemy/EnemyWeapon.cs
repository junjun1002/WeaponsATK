using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    /// <summary>�G�̃I�u�W�F�N�g�i�e�I�u�W�F�N�g�j</summary>
    [SerializeField] EnemyBase m_enemy;
    /// <summary>���ƏՓ˂������ɏo��G�t�F�N�g</summary>
    [SerializeField] ParticleSystem m_hitEffect;
    /// <summary>Player�̃I�u�W�F�N�g</summary>
    [SerializeField] VRPlayerController m_player;

    /// <summary>
    /// �U�������Ŏ󂯂�ꂽ�Ƃ��ɌĂ΂��
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
            Debug.Log("���炦");
            m_enemy.m_atkPoint = Random.Range(0.05f, 0.08f);
            Debug.Log(m_enemy.m_atkPoint);
            UIManager.Instance.DecreasesHPUI(m_enemy.m_atkPoint);
            m_player.m_playerHp -= m_enemy.m_atkPoint;
        }
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
