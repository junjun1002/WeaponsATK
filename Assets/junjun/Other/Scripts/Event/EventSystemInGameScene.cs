using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    public class EventSystemInGameScene : MonoBehaviour
    {
        public event Action<EnemyType> EnemyType;

        public void ExecuteEnemyEvent(EnemyType enemyType)
        {
            
        }

    }
}
