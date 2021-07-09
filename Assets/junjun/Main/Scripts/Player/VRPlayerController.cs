using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Junjun
{
    /// <summary>
    /// Player�𐧌䂷��N���X
    /// </summary>
    public class VRPlayerController : MonoBehaviour
    {
        ///<summary> SP</summary>
        [SerializeField] public int m_sp;


        /// <summary>HP</summary>
        [SerializeField] public float m_playerHp = 1f;
        /// <summary>Player�̃}�b�N�XHP</summary>
        float m_maxHp;

        /// <summary>�X���[���[�V�����̌��ʎ���</summary>
        [SerializeField] float m_zoneTime;
        /// <summary>�]�[����Ԃ̎��ɖڂ̑O�������������邽�߂�Image</summary>
        [SerializeField] Image m_zoneImage;

        /// <summary>Player���_���[�W���󂯂��ۂɎ��E�����߂Ă����̂Ɏg�p</summary>
        [SerializeField] Volume volume;

        private void Start()
        {
            m_maxHp = m_playerHp;
            volume.weight = 0f;
        }

        private void Update()
        {
            if (m_playerHp <= 0)
            {
                GameManager.Instance.GameOver();
                Debug.Log("GameOver");
            }

            volume.weight = 1f - (m_playerHp / m_maxHp);

            if (OVRInput.Get(OVRInput.Button.Start))
            {
                UIManager.Instance.ActiveUI();
            }

            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                if (m_sp >= 30)
                {
                    UIManager.Instance.UseSPUI(0.3f);
                    m_sp -= 30;
                    TimeState.Instance.SlowTime();
                    m_zoneImage.gameObject.SetActive(true);
                    SoundManager.Instance.ZoneTime();
                    Invoke("StopZone", m_zoneTime);
                    
                }
            }
        }

        /// <summary>
        /// �X���[���[�V������Ԃ��~�߂�
        /// </summary>
        void StopZone()
        {
            TimeState.Instance.RestoredTime();
            m_zoneImage.gameObject.SetActive(false);
            SoundManager.Instance.ResetAudioMixer();
        }
    }
}
