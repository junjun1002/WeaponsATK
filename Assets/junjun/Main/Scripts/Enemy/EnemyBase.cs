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
        ///<summary>補完スピードを決める</summary> 
        [SerializeField] public float m_lookSpeed = 0.1f;
        ///<summar>HP</summar> 
        [SerializeField] public int m_hp;
        ///<summary> Player(ターゲット)</summary>
        [SerializeField] protected GameObject m_player;
        ///<summary> 敵が止まる距離</summary>
        [SerializeField] public float m_stopDistance = 20;
        ///<summary> 攻撃力</summary>
        public float m_atkPoint;
        /// <summary>敵のアニメション</summary>
        public Animator m_anim;
        /// <summary>敵の武器オブジェクト</summary>
        [SerializeField] protected GameObject m_enemyWeapon;

        /// <summary>敵が死ぬときの演出</summary>
        [SerializeField] PlayableDirector m_enemyDie;
        /// <summary>敵が死ぬ時の演出エフェクト</summary>
        [SerializeField] GameObject m_dieEffect;

        /// <summary>PlayerとEnemyの距離 </summary>
        public float m_distance;

        public NavMeshAgent m_agent;


        /// <summary> 無敵状態の判定</summary>
        protected bool m_isInvincible = false;

        protected bool m_isOnDamage = false;

        /// <summary>ノックバックする力</summary>
        public Vector3 m_knockBackVelocity = Vector3.zero;
        /// <summary>ノックバックする力</summary>
        [SerializeField] protected float m_knockBackPower;
        public SkinnedMeshRenderer m_meshRenderer;



        /// <summary>EnemyのHPゲージImage</summary>
        [SerializeField] Image m_enemyHpGauge;
        /// <summary>EnemyのMaxHP</summary>
        int m_enemyMaxHp;

        virtual protected void Start()
        {
            m_atkPoint = UnityEngine.Random.Range(0.05f, 0.08f);
            m_enemyMaxHp = m_hp;
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

            if (m_hp <= 0)
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

        /// <summary>
        /// playerを追いかける
        /// アニメションがRunになった瞬間に呼ばれたいので
        /// アニメションイベントで呼び出すようにする
        /// </summary>
        public abstract void MoveToPlayer();

        ///// <summary>
        ///// 状態をidleにチェンジ
        ///// </summary>
        //protected async void EnemyStateIdle()
        //{
        //    if (m_enemyState == EnemyStateType.CoolTime)
        //    {
        //        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        //    }
        //    Debug.Log("idle");
        //    m_enemyState = EnemyStateType.Idle;
        //}

        ///// <summary>
        ///// 状態をCoolTimeにする
        ///// </summary>
        //protected void EnemyStateCoolTime()
        //{     
        //    m_enemyState = EnemyStateType.CoolTime;
        //    Debug.Log(m_enemyState);
        //}

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
            m_agent.isStopped = true;
            m_isInvincible = true;
            Debug.Log("ノックバック");
            m_meshRenderer.material.color = Color.red;
            m_anim.SetTrigger("Hit");
            m_knockBackVelocity = -transform.forward * m_knockBackPower;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            m_knockBackVelocity = Vector3.zero;
            m_meshRenderer.material.color = Color.white;
            m_anim.SetBool("Idle", true);
            m_isInvincible = false;
            m_isOnDamage = false;
        }

        /// <summary>
        /// EnemyのHPが減少した時にHPゲージを減少させる関数
        /// </summary>
        public void EnemyHPDecrease()
        {
            int currentHp = m_hp;
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
        public abstract void Parry();
    }
}

///// <summary>
///// Enemyの状態を表すenum
///// </summary>
//public enum EnemyStateType
//{
//    None, Idle, Chase, Attack, RangedATK, CoolTime, KnockBack
//}

///// <summary>
///// Enemyの種類を表すEnum
///// </summary>
//public enum EnemyType
//{
//    PunchingBag, Spider, Golem, Goblins, Tiger
//}
