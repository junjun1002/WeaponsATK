using UnityEngine;

namespace Junjun
{
    /// <summary>
    /// ゴブリンのステートのオーナー
    /// </summary>
    public class Goblins : EnemyBase
    {
        /// <summary>ゴブリンのステートマシン</summary>
        public StateMachine<Goblins> stateMachine;

        /// <summary>
        /// あれ？これGetだけでよかったけ？
        /// なんかど忘れ(笑）
        /// </summary>
        private IState<Goblins> idleState = new GoblinIdle();
        public IState<Goblins> IdleState { get => idleState; }

        private IState<Goblins> chaseState = new GoblinChase();
        public IState<Goblins> ChaseState { get => chaseState; }

        private IState<Goblins> attackState = new GoblinAttack();
        public IState<Goblins> AttackState { get => attackState; }

        IState<Goblins> DieState = new GoblinDie();

        protected override void Start()
        {
            base.Start();
            stateMachine = new StateMachine<Goblins>(this, IdleState);
        }

        protected override void Update()
        {
            base.Update();
            if (!m_isInvincible)
            {
                stateMachine.currentState.OnExecute(this);
            }
            if (m_currentHp <= 0)
            {
                stateMachine.ChageMachine(DieState);
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

        /// <summary>
        /// パリィされた時に呼ばれる関数
        /// </summary>
        public override void Parry()
        {
            if (stateMachine.currentState == AttackState)
            {
                m_anim.SetBool("Attack", false);
                m_anim.SetBool("Attack2", false);
                m_anim.SetTrigger("Hit");
                stateMachine.ChageMachine(IdleState);
            }
        }
        
    }

    /// <summary>
    /// Enemyが死んだときのステート（エラー回避用）
    /// </summary>
    class GoblinDie : IState<Goblins>
    {
        public void OnExecute(Goblins owner)
        {
            Debug.Log("僕は死にました。");
        }
    }
}
