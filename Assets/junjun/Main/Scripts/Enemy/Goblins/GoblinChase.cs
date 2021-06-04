using UnityEngine;

namespace Junjun
{
    public class GoblinChase : MonoBehaviour, IState<Goblins>
    {
        /// <summary>Chaseの時のアニメーション</summary>
        [SerializeField] AnimationClip m_chaseAnim;

        public void OnExcute(Goblins owner)
        {
            owner.LookAtPlayer();
            

            if (owner.m_distance <= owner.m_stopDistance)
            {
                owner.stateMachine.ChageMachine(owner.IdleState);
            }
        }
    }
}
