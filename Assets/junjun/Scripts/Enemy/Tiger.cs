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
        switch (m_enemyState)
        {
            case EnemyState.None:
                
                m_anim.SetBool("chase", false);
                m_anim.SetBool("punch", false);
                if (m_distance <= m_atkRange)
                {
                    MoveStop();
                    m_enemyState = EnemyState.Attack;
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
                    m_anim.SetBool("chase", false);
                    m_anim.SetBool("punch", false);

                    if (m_distance >= m_atkRange)
                    {
                        if (m_nextMove == 0)
                        {
                           
                            m_enemyState = EnemyState.Chase;
                        }
                        if (m_nextMove == 1)
                        {  
                            m_enemyState = EnemyState.RangedATK;
                        }
                    }
                    if (m_distance <= m_atkRange)
                    {
                       
                        m_enemyState = EnemyState.Attack;
                        break;
                    }
                }
                break;

            case EnemyState.Chase:

                m_anim.SetBool("chase", true);

                if (m_distance <= m_atkRange)
                {
                    Debug.Log("んあああ");
                    m_enemyState = EnemyState.None;
                    m_anim.SetBool("chase", false);
                }
                break;

            case EnemyState.Attack:
                m_anim.SetBool("punch", true);
                
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