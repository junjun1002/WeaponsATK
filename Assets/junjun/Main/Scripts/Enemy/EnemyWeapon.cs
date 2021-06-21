using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Junjun
{
    public class EnemyWeapon : MonoBehaviour
    {
        /// <summary>敵のオブジェクト（親オブジェクト）</summary>
        [SerializeField] EnemyBase m_enemy;
        /// <summary>盾と衝突した時に出るエフェクト</summary>
        [SerializeField] ParticleSystem m_hitEffect;
        /// <summary>Playerのオブジェクト</summary>
        [SerializeField] VRPlayerController m_player;

        /// <summary>パリィが成功したかどうか</summary>
        bool isParrySuccess = false;

        /// <summary>
        /// 攻撃を盾で受けられたときに呼ばれる
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
                 * パリィが成功しているのに武器の先っちょがあたりダメージが発生してしまう事象が発生していたので
                 * ダメージ処理を遅延実行させることでそれを解消
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
