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
        /// <summary>魔法の速度</summary>
        [SerializeField] float m_magicSpeed;
        /// <summary>魔法が存在する時間</summary>
        [SerializeField] int m_lifeTime;
        /// <summary>Plater</summary>
        [SerializeField] GameObject m_player;
        /// <summary>魔法の威力</summary>
        [SerializeField] float m_magicPower;
        #endregion

        Rigidbody m_rb;

        /// <summary>
        /// アクティブに切り替わった瞬間に飛んでm_lifeTime後に消える関数
        /// </summary>
        private async void OnEnable()
        {
            if (m_rb == null)
            {
                m_rb = GetComponent<Rigidbody>();
            }

            // 生成された瞬間だけPlayerを目標にして動く
            m_rb.velocity = (m_player.transform.position - transform.position) * m_magicSpeed;

            await UniTask.Delay(TimeSpan.FromSeconds(m_lifeTime));
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 魔法がPlayerもしくはShieldに当たった時の処理
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
