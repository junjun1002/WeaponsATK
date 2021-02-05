using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvent : MonoBehaviour,IEventCollision
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
        if (!(m_enemyType == EnemyType.PunchingBag))
        {
            eventSystem.ExecuteEnemyEvent(m_score, m_enemyType);
            gameObject.SetActive(false);
        }
        else
        {
            eventSystem.ExecuteEnemyEvent(m_score, m_enemyType);
        }
    }
}

public enum EnemyType
{
    PunchingBag, Enemy
}
