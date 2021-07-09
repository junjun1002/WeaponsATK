using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Junjun
{
    /// <summary>
    /// Playerを制御するクラス
    /// </summary>
    public class VRPlayerController : MonoBehaviour
    {
        ///<summary> SP</summary>
        [SerializeField] public int m_sp;


        /// <summary>HP</summary>
        [SerializeField] public float m_playerHp = 1f;
        /// <summary>PlayerのマックスHP</summary>
        float m_maxHp;

        /// <summary>スローモーションの効果時間</summary>
        [SerializeField] float m_zoneTime;

        /// <summary>Playerがダメージを受けた際に視界を狭めていくのに使用</summary>
        [SerializeField] Volume volume;

        [SerializeField] Image m_zoneImage;

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
                   // m_zoneImage.gameObject.SetActive(true);
                    Invoke("StopZone", m_zoneTime);
                   // m_zoneImage.gameObject.SetActive(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (m_sp >= 30)
                {
                    UIManager.Instance.UseSPUI(0.3f);
                    m_sp -= 30;
                    TimeState.Instance.SlowTime();
                  //  m_zoneImage.gameObject.SetActive(true);
                    Invoke("StopZone", m_zoneTime);
                   // m_zoneImage.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// スローモーション状態を止める
        /// </summary>
        void StopZone()
        {
            TimeState.Instance.RestoredTime();
        }
    }
}
