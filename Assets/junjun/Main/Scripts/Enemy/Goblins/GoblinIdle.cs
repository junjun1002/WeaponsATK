using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace Junjun
{
    /// <summary>
    /// ゴブリンのIdleステート
    /// </summary>
    public class GoblinIdle : IState<Goblins>
    {
        public async void OnExecute(Goblins owner)
        {
            if (owner.m_distance <= owner.m_agent.stoppingDistance)
            {
                owner.m_anim.SetBool("Attack", true);
                owner.stateMachine.ChageMachine(owner.AttackState);
            }
            owner.m_anim.SetBool("Idle", false);
            owner.m_agent.isStopped = true;
            await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
            if (owner.m_distance > owner.m_agent.stoppingDistance)
            {
                owner.m_anim.SetBool("Run", true);
                owner.stateMachine.ChageMachine(owner.ChaseState);      
            }
        }
    }
}
