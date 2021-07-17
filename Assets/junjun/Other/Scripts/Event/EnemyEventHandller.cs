using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    public class EnemyEventHandller : EventReceiver<EnemyEventHandller>
    {
        [SerializeField] EnemyBase enemyBase;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.TryGetComponent<IEventCollision>(out var eventCollision))
            {
                eventCollision.CollisionEvent(m_eventSystemInGameScene);
            }
        }
    }
}
