using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class Tiger : EnemyBase
{
    [SerializeField] PlayableDirector m_RangedATKDir;
    [SerializeField] PlayableDirector m_RunDir;
    int m_nextMove;
   
    protected override void Start()
    {
        base.Start();
        m_nextMove = Random.Range(0, 2);
        Debug.Log(m_nextMove);
    }

    protected override void Update()
    {
        base.Update();
        if (m_distance <= m_atkRange)
        {
            m_enemyState = EnemyState.None;
            MoveStop();
        }
        switch (m_enemyState)
        {
            case EnemyState.None:
                transform.position = transform.position;
                m_anim.SetBool("chase", false);
                if (m_distance <= m_atkRange)
                {
                    m_anim.SetBool("Punch", true);
                }
                if (m_distance >= m_atkRange)
                {
                    m_enemyState = EnemyState.Idle;
                    NextMove();
                }
                break;

            case EnemyState.Idle:
                if (m_anim)
                {               
                    MoveStop();
                    m_anim.SetBool("chase", false);
                   
                    
                    if (m_distance >= m_atkRange)
                    {
                        if (m_nextMove == 0)
                        {
                            MoveStop();
                            m_enemyState = EnemyState.Chase;
                        }
                        if (m_nextMove == 1)
                        {
                            MoveStop();
                            m_enemyState = EnemyState.RangedATK;
                        }
                    }
                    if (m_distance <= m_atkRange)
                    {
                        break;
                    }
                }
                break;

            case EnemyState.Chase:
                
                m_anim.SetBool("chase", true);
                if (m_distance <= m_atkRange)
                {
                    m_anim.SetBool("chase", false);
                }
                break;

            case EnemyState.Attack:
                m_anim.SetBool("Punch", true);
                m_anim.SetBool("idle", true);
                break;

            case EnemyState.RangedATK:
                TimelinePlayer.Instance.PlayTimeline(m_RangedATKDir);
                break;
           
            default:
                break;
        }
    }

    void NextMove()
    {
        m_nextMove = Random.Range(0, 2);
        Debug.Log(m_nextMove);
    }
}