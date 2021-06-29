using UnityEngine;

namespace Junjun
{
    /// <summary>
    /// ゴブリンのAttackステート
    /// </summary>
    public class GoblinAttack : IState<Goblins>
    {
        public void OnExecute(Goblins owner)
        {
            if (owner.m_nextAtk == 0)
            {
                owner.m_anim.SetBool("Attack", true);
            }
            else if (owner.m_nextAtk == 1)
            {
                owner.m_anim.SetBool("Attack2", true);
            }

            if (owner.m_distance > owner.m_agent.stoppingDistance)
            {
                owner.m_anim.SetBool("Attack", false);
                owner.m_anim.SetBool("Attack2", false);
                owner.m_anim.SetBool("Run", true);
                owner.stateMachine.ChageMachine(owner.ChaseState);
            }
        }
    }
}
