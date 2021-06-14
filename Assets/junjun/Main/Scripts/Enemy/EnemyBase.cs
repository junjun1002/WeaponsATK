using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

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


        /// <summary>PlayerとEnemyの距離 </summary>
        public float m_distance;

        public NavMeshAgent m_agent;


        /// <summary> 無敵状態の判定</summary>
        protected bool m_isInvincible;

        /// <summary>ノックバックする力</summary>
        protected Vector3 m_knockBackVelocity = Vector3.zero;
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
            if (m_hp <= 0)
            {
                GameManager.Instance.GameClear();
            }
           
        }


        /// <summary>
        /// playerを追いかける
        /// </summary>
        public abstract void MoveToPlayer();

        /// <summary>
        /// ダメージを受けた時にノックバックする
        /// </summary>
        public abstract void KnockBack();

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



        ///// <summary>
        ///// EnemyのHPが減少した時にHPゲージを減少させる関数
        ///// </summary>
        //public void EnemyHPDecrease()
        //{
        //    int currentHp = m_hp;
        //    float hpRatio = (float)currentHp / (float)m_enemyMaxHp;

        //    m_enemyHpGauge.fillAmount = hpRatio;
        //    if (m_enemyHpGauge.fillAmount <= 0.5f)
        //    {
        //        m_enemyHpGauge.color = Color.yellow;
        //    }
        //    if (m_enemyHpGauge.fillAmount <= 0.15f)
        //    {
        //        m_enemyHpGauge.color = Color.red;
        //    }
        //}

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
