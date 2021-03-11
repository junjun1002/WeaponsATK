using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvent : MonoBehaviour,IEventCollision
{
    [SerializeField] EnemyType m_enemyType;
  
    [SerializeField] int m_hp;

    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        if (!(m_enemyType == EnemyType.PunchingBag))
        {
            eventSystem.ExecuteEnemyEvent(m_enemyType);
            if (m_hp <= 0)
            {
                gameObject.SetActive(false);
            }    
        }
        else
        {
            Debug.Log("痛いンゴ");
            eventSystem.ExecuteEnemyEvent(m_enemyType);
        }
    }
}



