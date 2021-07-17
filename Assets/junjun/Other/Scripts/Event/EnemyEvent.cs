using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    public class EnemyEvent : MonoBehaviour, IEventCollision
    {
        [SerializeField] EnemyType m_enemyType;

        [SerializeField] EnemyBase enemyBase;

        public void CollisionEvent(EventSystemInGameScene eventSystem)
        {
            enemyBase.EnemyHPDecrease();
            enemyBase.KnockBack();
        }
    }

    public enum EnemyType
    {
        Goblin, Witch
    }
}

