using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Junjun
{
    /// <summary>
    /// ゴブリンのステートのオーナー
    /// </summary>
    public class Goblins : EnemyBase
    {
        /// <summary>ゴブリンのステートマシン</summary>
        public StateMachine<Goblins> stateMachine;

        public IState<Goblins> IdleState { get; set; } = new GoblinIdle();
        public IState<Goblins> ChaseState { get; set; } = new GoblinChase();
        public IState<Goblins> AttackState { get; set; } = new GoblinAttack();

        protected override void Start()
        {
            base.Start();
            stateMachine = new StateMachine<Goblins>(this, IdleState);
        }

        protected override void Update()
        {
            base.Update();
            if (!m_isOnDamage)
            {
                stateMachine.currentState.OnExecute(this);
            }
        }

        /// <summary>
        /// playerを追いかける
        /// </summary
        public override void MoveToPlayer()
        {
            m_agent.SetDestination(m_player.transform.position);
            m_agent.isStopped = false;
        }

        public override void Parry()
        {
            if (stateMachine.currentState == AttackState)
            {
                m_anim.SetTrigger("Hit");
            }
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
}
