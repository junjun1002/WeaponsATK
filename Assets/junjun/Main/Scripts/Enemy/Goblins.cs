using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class Goblins : EnemyBase
{
    /// <summary>Ÿ‚ÌUŒ‚‚Ìí—Ş</summary>
    int m_nextAttack;
    protected override void Start()
    {
        base.Start();
        m_nextAttack = Random.Range(0, 2);
    }

    protected override void Update()
    {

        base.Update();

        switch (m_enemyState)
        {
            case EnemyStateType.None:
                break;
            case EnemyStateType.Idle:
                if (m_anim)
                {
                    m_anim.SetBool("Chase", false);
                    m_anim.SetBool("Attack", false);
                    m_anim.SetBool("Thrust", false);
                    m_anim.SetBool("Hit", false);
                    
                    if (m_distance >= m_atkRange)
                    {
                        m_enemyState = EnemyStateType.Chase;
                    }
                    else
                    {
                        m_enemyState = EnemyStateType.Attack;
                    }
                }
                break;
            case EnemyStateType.Chase:
                LookAtPlayer();
                m_anim.SetBool("Chase", true);

                if (m_distance <= m_atkRange)
                {
                    m_enemyState = EnemyStateType.Idle;
                }
                break;
            case EnemyStateType.Attack:

                LookAtPlayer();
                break;
            case EnemyStateType.RangedATK:
                break;
            case EnemyStateType.CoolTime:
                    
                m_enemyState = EnemyStateType.Idle;
                break;
            case EnemyStateType.KnockBack:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Ÿ‚ÌUŒ‚s“®
    /// </summary>
    void NextAttack()
    {
        if (m_distance <= m_atkRange)
        {
            m_nextAttack = Random.Range(0, 2);
            Debug.Log(m_nextAttack);
            m_enemyState = EnemyStateType.Attack;
            if (m_nextAttack == 0)
            {
                m_anim.SetBool("Attack", true);
            }
            else if (m_nextAttack == 1)
            {
                m_anim.SetBool("Thrust", true);
            }
        }
    }
}
