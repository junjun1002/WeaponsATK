using UnityEngine;

namespace Junjun
{
    public class GoblinChase :IState<Goblins>
    {

        public void OnExcute(Goblins owner)
        {
            owner.LookAtPlayer();
            owner.MoveToPlayer();
           
            if (owner.m_distance < owner.m_agent.stoppingDistance)
            {
                owner.m_anim.SetBool("Idle", true);
               // owner.MoveStop();
                owner.m_anim.SetBool("Run", false);
                owner.stateMachine.ChageMachine(owner.IdleState);
            }
        }
    }
}
