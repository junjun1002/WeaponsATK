using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Junjun
{
    public class Goblins : EnemyBase
    {
        public StateMachine<Goblins> stateMachine;

        /// <summary>敵のアニメション</summary>
        public Animator m_anim;


        public IState<Goblins> IdleState { get; set; } = new GoblinIdle();

        public IState<Goblins> ChaseState { get; set; } = new GoblinChase();

        public IState<Goblins> AttackState { get; set; } = new GoblinAttack();

        public IState<Goblins> DamageState { get; set; } = new GoblinDamage();

        protected override void Start()
        {
            base.Start();
            stateMachine = new StateMachine<Goblins>(this, IdleState);
        }

        protected override void Update()
        {
            if (!(stateMachine.currentState == DamageState))
            {
                base.Update();
                stateMachine.currentState.OnExcute(this);
            }
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.tag == "Sword")
        //    {
        //        stateMachine.currentState.OnExcute(this);
        //    }
        //}

        /// <summary>
        /// playerを追いかける
        /// </summary
        public override void MoveToPlayer()
        {
            m_agent.SetDestination(m_player.transform.position);
            if (stateMachine.currentState == ChaseState)
            {
                m_agent.isStopped = false;
            }
        }

        /// <summary>
        /// ダメージを受けた時にノックバックする
        /// </summary>
        public override async void KnockBack()
        {
            /// 多段ヒットしないように攻撃を受けて少しの間は無敵化
            if (m_isInvincible)
            {
                return;
            }
            m_isInvincible = true;
            stateMachine.ChageMachine(DamageState);
            Debug.Log("ノックバック");
            m_knockBackVelocity = -transform.forward * m_knockBackPower;
            m_meshRenderer.material.color = Color.red;
            m_anim.SetBool("Hit", true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            m_knockBackVelocity = Vector3.zero;
            m_meshRenderer.material.color = Color.white;
            stateMachine.ChageMachine(IdleState);
            m_anim.SetBool("Hit", false);
            m_anim.SetBool("Idle", true);
            m_isInvincible = false;
        }

        ///// <summary>次の攻撃の種類</summary>
        //int m_nextAttack;
        //protected override void Start()
        //{
        //    base.Start();
        //    m_nextAttack = Random.Range(0, 2);
        //}

        //protected override void Update()
        //{

        //    base.Update();

        //    switch (m_enemyState)
        //    {
        //        case EnemyStateType.None:
        //            break;
        //        case EnemyStateType.Idle:
        //            if (m_anim)
        //            {
        //                m_anim.SetBool("Chase", false);
        //                m_anim.SetBool("Attack", false);
        //                m_anim.SetBool("Thrust", false);
        //                m_anim.SetBool("Hit", false);

        //                if (m_distance >= m_atkRange)
        //                {
        //                    m_enemyState = EnemyStateType.Chase;
        //                }
        //                else
        //                {
        //                    m_enemyState = EnemyStateType.Attack;
        //                }
        //            }
        //            break;
        //        case EnemyStateType.Chase:
        //            LookAtPlayer();
        //            m_anim.SetBool("Chase", true);

        //            if (m_distance <= m_atkRange)
        //            {
        //                m_enemyState = EnemyStateType.Idle;
        //            }
        //            break;
        //        case EnemyStateType.Attack:

        //            LookAtPlayer();
        //            break;
        //        case EnemyStateType.RangedATK:
        //            break;
        //        case EnemyStateType.CoolTime:

        //            m_enemyState = EnemyStateType.Idle;
        //            break;
        //        case EnemyStateType.KnockBack:
        //            break;
        //        default:
        //            break;
        //    }
        //}

        ///// <summary>
        ///// 次の攻撃行動
        ///// </summary>
        //void NextAttack()
        //{
        //    if (m_distance <= m_atkRange)
        //    {
        //        m_nextAttack = Random.Range(0, 2);
        //        Debug.Log(m_nextAttack);
        //        m_enemyState = EnemyStateType.Attack;
        //        if (m_nextAttack == 0)
        //        {
        //            m_anim.SetBool("Attack", true);
        //        }
        //        else if (m_nextAttack == 1)
        //        {
        //            m_anim.SetBool("Thrust", true);
        //        }
        //    }
        //}
    }

    class GoblinDamage : IState<Goblins>
    {
        public void OnExcute(Goblins owner)
        {
            owner.KnockBack();
        }
    }
}
