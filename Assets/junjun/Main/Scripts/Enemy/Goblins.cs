using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            case EnemyState.None:
                break;
            case EnemyState.Idle:
                if (m_anim)
                {
                    m_anim.SetBool("Chase", false);
                    m_anim.SetBool("Attack", false);
                    m_anim.SetBool("Thrust", false);
                    if (m_distance >= m_atkRange)
                    {
                        m_enemyState = EnemyState.Chase;
                    }
                    else
                    {
                        m_enemyState = EnemyState.Attack;
                    }
                }
                break;
            case EnemyState.Chase:
                LookAtPlayer();
                m_anim.SetBool("Chase", true);

                if (m_distance <= m_atkRange)
                {
                    m_enemyState = EnemyState.Idle;
                }
                break;
            case EnemyState.Attack:

                LookAtPlayer();
                break;
            case EnemyState.RangedATK:
                break;
            case EnemyState.CoolTime:
                m_enemyState = EnemyState.Idle;
                break;
            default:
                break;
        }
    }

    void NextAttack()
    {
        if (m_distance <= m_atkRange)
        {
            m_nextAttack = Random.Range(0, 2);
            Debug.Log(m_nextAttack);
            m_enemyState = EnemyState.Attack;
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
