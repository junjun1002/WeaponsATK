using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    public class Goblins : EnemyBase
    {
        public StateMachine<Goblins> stateMachine;

        public Animation m_anim;

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
            stateMachine.currentState.OnExcute(this);
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

    public class GoblinAttack : IState<Goblins>
    {
        /// <summary>Attackの時のアニメーション</summary>
        [SerializeField] Animation m_attackAnim;
        public void OnExcute(Goblins owner)
        {
            
        }
    }
}
