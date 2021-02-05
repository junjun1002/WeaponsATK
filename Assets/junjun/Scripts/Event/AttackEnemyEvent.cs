using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyEvent : MonoBehaviour, IEventCollision
{
    [SerializeField] EnemyType m_enemyType;
    [SerializeField] int m_score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        eventSystem.ExecuteAttackEnemyEvent(m_score, m_enemyType);

        if (!(m_enemyType == EnemyType.PunchingBag))
        {
            gameObject.SetActive(false);
        }
    }
}

public enum EnemyType
{
    PunchingBag
}
