﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class Tiger : EnemyBase
{
    [SerializeField] PlayableDirector m_RangedATKDir;

    int m_nextMove;
    int m_nextAttack;

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
                    m_nextAttack = Random.Range(0, 3);
                    Debug.Log("次の攻撃は" + m_nextAttack);
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
                    m_anim.SetBool("biting", false);
                    m_anim.SetBool("roar", false);

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
                }
                break;

            case EnemyState.Chase:
                SoundManager.Instance.PlayTigerSE(SoundManager.Instance.m_runSE);
                m_anim.SetBool("chase", true);

                if (m_distance <= m_atkRange)
                {
                    Debug.Log("んあああ");
                    m_enemyState = EnemyState.None;
                    m_anim.SetBool("chase", false);
                }
                break;

            case EnemyState.Attack:
                Debug.Log("食らいやがれぇぇええ！");                
                break;

            case EnemyState.RangedATK:
                TimelinePlayer.Instance.PlayTimeline(m_RangedATKDir);
                break;

            case EnemyState.CoolTime:
                if (m_distance <= m_atkRange)
                {
                    m_enemyState = EnemyState.Idle;
                }
                if (m_distance >= m_atkRange)
                {
                    NextMove();
                    m_enemyState = EnemyState.Idle;
                }
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

    protected void ChangeAnim()
    {
        if (m_distance <= m_atkRange)
        {
            m_nextAttack = Random.Range(0, 3);
            Debug.Log("次の攻撃は" + m_nextAttack);
            m_enemyState = EnemyState.Attack;
        }
        if (m_enemyState == EnemyState.Attack)
        {
            if (m_nextAttack == 0)
            {
                m_anim.SetBool("punch", true);
            }
            if (m_nextAttack == 1)
            {
                m_anim.SetBool("biting", true);
            }
            if (m_nextAttack == 2)
            {
                m_anim.SetBool("roar", true);
            }
        }
    }
}