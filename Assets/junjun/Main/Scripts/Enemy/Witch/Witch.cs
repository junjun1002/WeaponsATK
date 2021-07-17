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

        /// <summary>���@�𐶐�����ꏊ</summary>
        [SerializeField] GameObject m_magicSpwner;
        /// <summary>�U�����@�̃I�u�W�F�N�g</summary>
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
        /// �A�j���[�V�����ɍ��킹�čU������֐�
        /// �A�j���[�V�����C�x���g�ŌĂ�
        /// </summary>
        public void OnAttack()
        {
            m_fire.transform.position = m_magicSpwner.transform.position;
            m_fire.gameObject.SetActive(true);
        }

        /// <summary>
        /// �A�j���[�V�����C�x���g�ŃX�e�[�g�̐؂�ւ����s���֐�
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
