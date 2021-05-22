using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 虎の敵の継承クラス
/// </summary>
public class Tiger : EnemyBase
{
    /// <summary>遠距離攻撃のタイムライン</summary>
    [SerializeField] PlayableDirector m_rangedATKDir;
    /// <summary>咆哮のタイムライン</summary>
    [SerializeField] PlayableDirector m_roarDir;
    
    /// <summary>次の行動の判定</summary>
    int m_nextMove;
    /// <summary>次の攻撃の判定</summary>
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
            case EnemyStateType.None:

                m_anim.SetBool("chase", false);
                m_anim.SetBool("punch", false);
                if (m_distance <= m_atkRange)
                {
                    MoveStop();
                    m_nextAttack = Random.Range(0, 3);
                    Debug.Log("次の攻撃は" + m_nextAttack);
                    m_enemyState = EnemyStateType.Attack;
                }
                if (m_distance >= m_atkRange)
                {
                    m_enemyState = EnemyStateType.Idle;
                    NextMove();
                }
                break;

            case EnemyStateType.Idle:

                if (m_anim)
                {
                    m_anim.SetBool("chase", false);
                    m_anim.SetBool("punch", false);
                    m_anim.SetBool("biting", false);

                    if (m_distance >= m_atkRange)
                    {
                        if (m_nextMove == 0)
                        {
                            m_enemyState = EnemyStateType.Chase;
                        }
                        if (m_nextMove == 1)
                        {
                            m_enemyState = EnemyStateType.RangedATK;
                        }
                    }
                }
                break;

            case EnemyStateType.Chase:
                LookAtPlayer();
                
                m_anim.SetBool("chase", true);

                if (m_distance <= m_atkRange)
                {
                    Debug.Log("んあああ");
                    m_enemyState = EnemyStateType.None;
                    m_anim.SetBool("chase", false);
                }
                break;

            case EnemyStateType.Attack:
                LookAtPlayer();
                
                break;

            case EnemyStateType.RangedATK:
                LookAtPlayer();
                TimelinePlayer.Instance.PlayTimeline(m_rangedATKDir);
                break;

            case EnemyStateType.CoolTime:
                if (m_distance <= m_atkRange)
                {
                    m_enemyState = EnemyStateType.Idle;
                }
                if (m_distance >= m_atkRange)
                {
                    NextMove();
                    m_enemyState = EnemyStateType.Idle;
                }
                break;
            case EnemyStateType.KnockBack:
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Playerと距離が離れているときに次の行動に移るための関数（アニメーションイベントで呼ぶ）
    /// </summary>
    void NextMove()
    {
        m_nextMove = Random.Range(0, 2);
        Debug.Log(m_nextMove);
    }

    /// <summary>
    /// Playerが攻撃範囲内にいるときに次に起こす行動を決める関数
    /// </summary>
    protected void ChangeAnim()
    {
        if (m_distance <= m_atkRange)
        {
            m_nextAttack = Random.Range(0, 3);
            Debug.Log("次の攻撃は" + m_nextAttack);
            m_enemyState = EnemyStateType.Attack;
        }
        if (m_enemyState == EnemyStateType.Attack)
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
                TimelinePlayer.Instance.PlayTimeline(m_roarDir);
            }
        }
    }
}