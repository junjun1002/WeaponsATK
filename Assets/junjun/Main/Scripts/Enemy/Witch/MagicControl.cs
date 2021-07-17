using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace Junjun
{
    public class MagicControl : MonoBehaviour
    {
        #region SerializeField
        /// <summary>���@�̑��x</summary>
        [SerializeField] float m_magicSpeed;
        /// <summary>���@�����݂��鎞��</summary>
        [SerializeField] int m_lifeTime;
        /// <summary>Plater</summary>
        [SerializeField] GameObject m_player;
        /// <summary>���@�̈З�</summary>
        [SerializeField] float m_magicPower;
        #endregion

        Rigidbody m_rb;

        /// <summary>
        /// �A�N�e�B�u�ɐ؂�ւ�����u�Ԃɔ���m_lifeTime��ɏ�����֐�
        /// </summary>
        private async void OnEnable()
        {
            if (m_rb == null)
            {
                m_rb = GetComponent<Rigidbody>();
            }

            // �������ꂽ�u�Ԃ���Player��ڕW�ɂ��ē���
            m_rb.velocity = (m_player.transform.position - transform.position) * m_magicSpeed;

            await UniTask.Delay(TimeSpan.FromSeconds(m_lifeTime));
            gameObject.SetActive(false);
        }

        /// <summary>
        /// ���@��Player��������Shield�ɓ����������̏���
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Shield")
            {
                gameObject.SetActive(false);
            }
            else if (other.gameObject.TryGetComponent<VRPlayerController>(out var vRPlayerController))
            {
                UIManager.Instance.DecreasesHPUI(m_magicPower);
                vRPlayerController.m_playerHp -= m_magicPower;
                gameObject.SetActive(false);
            }
        }
    }
}
