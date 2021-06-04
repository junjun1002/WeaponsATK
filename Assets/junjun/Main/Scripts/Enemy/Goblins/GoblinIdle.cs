using UnityEngine;

namespace Junjun
{
    public class GoblinIdle : MonoBehaviour, IState<Goblins>
    {
        /// <summary>Idleの時のアニメーション</summary>
        [SerializeField] AnimationClip m_idleAnim;
        
        public void OnExcute(Goblins owner)
        {
            

            if (owner.m_distance >= owner.m_stopDistance)
            {
                owner.stateMachine.ChageMachine(owner.ChaseState);
            }
            else
            {
                owner.stateMachine.ChageMachine(owner.AttackState);
            }
        }
    }
}
