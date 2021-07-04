using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Junjun
{
    public class EnemyWeapon : MonoBehaviour
    {
        /// <summary>�G�̃I�u�W�F�N�g�i�e�I�u�W�F�N�g�j</summary>
        [SerializeField] EnemyBase m_enemy;
        /// <summary>���ƏՓ˂������ɏo��G�t�F�N�g</summary>
        [SerializeField] ParticleSystem m_hitEffect;
        /// <summary>Player�̃I�u�W�F�N�g</summary>
        [SerializeField] VRPlayerController m_player;

        /// <summary>�p���B�������������ǂ���</summary>
        bool isParrySuccess = false;

        /// <summary>
        /// �U�������Ŏ󂯂�ꂽ�Ƃ��ɌĂ΂��
        /// </summary>
        /// <param name="other"></param>
        private async void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Shield")
            {
                isParrySuccess = true;
                m_hitEffect.gameObject.SetActive(false);
                m_enemy.Parry();
                m_hitEffect.gameObject.SetActive(true);
            }

            if (other.gameObject.tag == "Player")
            {
                /*
                 * �p���B���������Ă���̂ɕ���̐�����傪������_���[�W���������Ă��܂����ۂ��������Ă����̂�
                 * �_���[�W������x�����s�����邱�Ƃł��������
                 */
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                if (isParrySuccess)
                {
                    isParrySuccess = false;
                    return;
                }
                m_enemy.m_atkPoint = UnityEngine.Random.Range(0.05f, 0.08f);
                Debug.Log(m_enemy.m_atkPoint);
                UIManager.Instance.DecreasesHPUI(m_enemy.m_atkPoint);
                m_player.m_playerHp -= m_enemy.m_atkPoint;
            }
        }

        
    }

}
