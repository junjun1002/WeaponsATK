using UnityEngine;

namespace Junjun
{
    public class GoblinIdle : IState<Goblins>
    {
        public void OnExcute(Goblins owner)
        {
            owner.m_anim.SetBool("Idle", false);
            if (owner.m_distance > owner.m_agent.stoppingDistance)
            {
                owner.m_anim.SetBool("Run", true);
                owner.stateMachine.ChageMachine(owner.ChaseState);      
            }
            else
            {
                owner.m_anim.SetBool("Attack", true);
                owner.stateMachine.ChageMachine(owner.AttackState);
            }
        }
    }
}
