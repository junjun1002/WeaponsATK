using UnityEngine;

namespace Junjun
{
    /// <summary>
    /// ゴブリンのChase（Playerを追いかける）ステート
    /// </summary>
    public class GoblinChase :IState<Goblins>
    {
        public void OnExecute(Goblins owner)
        {
            owner.LookAtPlayer();
            owner.MoveToPlayer();
           
            if (owner.m_distance < owner.m_agent.stoppingDistance)
            {
                owner.m_anim.SetBool("Idle", true);
                
                owner.m_anim.SetBool("Run", false);
                owner.stateMachine.ChageMachine(owner.IdleState);
            }
        }
    }
}
