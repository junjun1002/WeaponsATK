using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.Playables;

namespace Junjun
{
    /// <summary>
    /// Enemyの基底クラス
    /// </summary>
    public abstract class EnemyBase : MonoBehaviour
    {
        /// <summary>EnemyのDataを保持したScriptableObject</summary>
        [SerializeField] EnemyData enemyData;

        /// <summary>EnemyのHPゲージImage</summary>
        [SerializeField] Image m_enemyHpGauge;
        /// <summary>EnemyのMaxHP</summary>
        int m_enemyMaxHp;
        ///<summar>HP</summar> 
        [SerializeField, HideInInspector] public int m_currentHp;
        ///<summary> 攻撃力</summary>
        [SerializeField, HideInInspector] public float m_power;

        /// <summary>次の攻撃の種別判定</summary>
        [SerializeField, HideInInspector] public int m_nextAtk;

        ///<summary>ターゲットを見る補完スピードを決める</summary> 
        const float m_lookSpeed = 0.1f;

        /// <summary>PlayerとEnemyの距離 </summary>
        [SerializeField, HideInInspector] public float m_distance;

        ///<summary> Player(ターゲット)</summary>
        [SerializeField] protected GameObject m_player;

        /// <summary>敵のアニメション</summary>
        public Animator m_anim;

        [SerializeField] public NavMeshAgent m_agent;

        /// <summary> 無敵状態の判定</summary>
        protected bool m_isInvincible = false;

        /// <summary>敵が死ぬときの演出</summary>
        [SerializeField] PlayableDirector m_enemyDie;
        /// <summary>敵が死ぬ時の演出エフェクト</summary>
        [SerializeField] GameObject m_dieEffect;

        /// <summary>ノックバックする速度</summary>
        Vector3 m_knockBackVelocity = Vector3.zero;
        /// <summary>ノックバックする力</summary>
        [SerializeField] float m_knockBackPower;
        [SerializeField] SkinnedMeshRenderer m_meshRenderer;

        virtual protected void Start()
        {
            m_power = UnityEngine.Random.Range(0.05f, 0.08f);
            Init();
        }

        virtual protected void Update()
        {
            // playerと自分の距離を測る
            m_distance = Vector3.Distance(transform.position, m_player.transform.position);

            // ノックバックする
            if (m_knockBackVelocity != Vector3.zero)
            {
                m_agent.Move(m_knockBackVelocity * Time.deltaTime);
            }

            if (m_currentHp <= 0)
            {
                m_dieEffect.transform.position = this.transform.position;
                TimeLinePlayer.PlayTimeline(m_enemyDie);
                /*
                 * 死ぬ演出をさせる時にスクリプト的には非アクティブ状態にいないと動き出すため
                 * 無理やり実装
                 */
                this.gameObject.SetActive(false);
            }

        }

        void Init()
        {
            m_enemyMaxHp = enemyData.hp;
            m_currentHp = m_enemyMaxHp;
            m_power = enemyData.power;
        }

        /// <summary>
        /// playerを追いかける
        /// アニメションがRunになった瞬間に呼ばれたいので
        /// アニメションイベントで呼び出すようにする
        /// </summary>
        public virtual void MoveToPlayer() { }

        /// <summary>
        /// 滑らかにPlayerの方向を向くように
        /// </summary>
        public void LookAtPlayer()
        {
            // ターゲット方向のベクトルを取得
            Vector3 relativePos = m_player.transform.position - this.transform.position;
            // 方向を、回転情報に変換
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            // 現在の回転情報と、ターゲット方向の回転情報を補完する
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, m_lookSpeed);
        }

        /// <summary>
        /// ダメージを受けた時にノックバックする
        /// </summary>
        public async void KnockBack()
        {
            /// 多段ヒットしないように攻撃を受けて少しの間は無敵化
            if (m_isInvincible)
            {
                return;
            }

            if (m_agent.isOnNavMesh)
            {
                m_agent.isStopped = true;
            }

            m_isInvincible = true;
            Debug.Log("ノックバック");
            Debug.Log(m_meshRenderer);
            m_meshRenderer.material.color = Color.red;
            // m_anim.SetTrigger("Hit");
            m_knockBackVelocity = -transform.forward * m_knockBackPower;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            m_knockBackVelocity = Vector3.zero;
            m_meshRenderer.material.color = Color.white;

            m_isInvincible = false;
        }

        /// <summary>
        /// EnemyのHPが減少した時にHPゲージを減少させる関数
        /// </summary>
        public void EnemyHPDecrease()
        {
            int currentHp = m_currentHp;
            float hpRatio = (float)currentHp / (float)m_enemyMaxHp;

            m_enemyHpGauge.fillAmount = hpRatio;
            if (m_enemyHpGauge.fillAmount <= 0.5f)
            {
                m_enemyHpGauge.color = Color.yellow;
            }
            if (m_enemyHpGauge.fillAmount <= 0.15f)
            {
                m_enemyHpGauge.color = Color.red;
            }
        }

        /// <summary>
        /// Playerに盾で弾かれたときに呼ばれる関数
        /// </summary>
        public virtual void Parry() { }
    }
}
