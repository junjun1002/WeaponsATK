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

        /// <summary>ñÇñ@Çê∂ê¨Ç∑ÇÈèÍèä</summary>
        [SerializeField] GameObject m_magicSpwner;

        protected override void Start()
        {
            base.Start();
            stateMachine = new StateMachine<Witch>(this, IdleState);
            stateMachine.currentState.OnExecute(this);
        }

        protected override void Update()
        {
            base.Update();
            stateMachine.currentState.OnExecute(this);
        }
    }

    class WitchIdle : IState<Witch>
    {
        public void OnExecute(Witch owner)
        {
            owner.m_anim.SetBool("Idle", false);           
            owner.stateMachine.ChageMachine(owner.AttackState);
        }
    }

    class WitchAttack : IState<Witch>
    {
        public void OnExecute(Witch owner)
        {
            owner.m_anim.SetTrigger("Attack1");
            owner.stateMachine.ChageMachine(owner.IdleState);
            owner.m_anim.SetBool("Idle", true);
        }
    }
}
