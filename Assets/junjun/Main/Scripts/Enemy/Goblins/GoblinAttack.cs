using UnityEngine;

namespace Junjun
{
    public class GoblinAttack : IState<Goblins>
    {
        public void OnExcute(Goblins owner)
        {
            
            if (owner.m_distance > owner.m_agent.stoppingDistance)
            {
                owner.m_anim.SetBool("Attack", false);
                owner.m_anim.SetBool("Run", true);
                owner.stateMachine.ChageMachine(owner.ChaseState);
            }
        }
    }
}
