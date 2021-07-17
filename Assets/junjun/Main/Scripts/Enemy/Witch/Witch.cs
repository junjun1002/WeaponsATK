using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    public class Witch : EnemyBase
    {
        #region State
        public StateMachine<Witch> stateMachine;

        private IState<Witch> idleState = new WitchIdle();
        public IState<Witch> IdleState { get => idleState; }

        private IState<Witch> attackState = new WitchAttack();
        public IState<Witch> AttackState { get => attackState; }
        #endregion

        /// <summary>魔法を生成する場所</summary>
        [SerializeField] GameObject m_magicSpwner;
        /// <summary>攻撃魔法のオブジェクト</summary>
        [SerializeField] GameObject m_fire;

        protected override void Start()
        {
            base.Start();
            stateMachine = new StateMachine<Witch>(this, IdleState);
            stateMachine.currentState.OnExecute(this);
        }

        protected override void Update()
        {
            base.Update();
            LookAtPlayer();
        }

        /// <summary>
        /// アニメーションに合わせて攻撃する関数
        /// アニメーションイベントで呼ぶ
        /// </summary>
        public void OnAttack()
        {
            m_fire.transform.position = m_magicSpwner.transform.position;
            m_fire.gameObject.SetActive(true);
        }

        /// <summary>
        /// アニメーションイベントでステートの切り替えを行う関数
        /// </summary>
        /// <param name="state"></param>
        public void OnChangeState()
        {
            if (stateMachine.currentState == IdleState)
            {
                stateMachine.ChageMachine(AttackState);
            }
            else if (stateMachine.currentState == AttackState)
            {
                stateMachine.ChageMachine(IdleState);
            }
            stateMachine.currentState.OnExecute(this);
        }
    }

    class WitchIdle : IState<Witch>
    {
        public void OnExecute(Witch owner)
        {
            owner.m_anim.SetBool("Attack1", false);
            owner.m_anim.SetBool("Idle", true);
        }
    }

    class WitchAttack : IState<Witch>
    {
        public void OnExecute(Witch owner)
        {
            owner.m_anim.SetBool("Idle", false);
            owner.m_anim.SetBool("Attack1", true);
        }
    }
}
