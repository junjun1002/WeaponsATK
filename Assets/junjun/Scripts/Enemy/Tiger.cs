using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Tiger : EnemyBase
{
    [SerializeField] PlayableDirector m_RangedATKDir;
    [SerializeField] PlayableDirector m_RunDir;
   
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        switch (m_enemyState)
        {
            case EnemyState.Idle:
                velocity = Vector3.zero;
                if (m_anim)
                {
                    m_anim.SetBool("chase", false);
                    m_anim.SetBool("idle", true);
                    int random = Random.Range(0, 2);
                    if (m_distance >= m_atkRange)
                    {
                        if (random == 0)
                        {
                            m_enemyState = EnemyState.Chase;
                        }
                        if (random == 1)
                        {
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
                MoveToPlayer();
                if (m_distance <= m_atkRange)
                {
                    m_anim.SetBool("chase", false);
                }
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.RangedATK:
                TimelinePlayer.Instance.PlayTimeline(m_RangedATKDir);
                m_enemyState = EnemyState.Idle;
                break;
           
            default:
                break;
        }
        //if (m_readyToATK)
        //{
        //    if (m_distance >= m_atkRange && m_outOfRangeMove == false)
        //    {
        //        m_outOfRangeMove = true;
        //    }
        //    // playerが攻撃範囲内にいるとき
        //    if (m_distance <= m_atkRange)
        //    {
        //        velocity = Vector3.zero;
        //        m_run = false;
        //        m_enemyState = EnemyState.Idle;
        //    }
        //    if (m_enemyState == EnemyState.Chase)
        //    {
        //        if (m_run)
        //        {
        //            MoveToPlayer();
        //        }
        //    }
        //    if (m_outOfRangeMove)
        //    {
        //        Debug.Log("んご");
        //        m_outOfRangeMove = false;
        //        // playerとの距離が攻撃範囲よりも遠い時
        //        if (m_distance >= m_atkRange)
        //        {                  
        //            int randam = Random.Range(0, 2);
        //            Debug.Log(randam);
        //            if (randam == 0)
        //            {
        //                // playerを追いかける状態に入る
        //                m_enemyState = EnemyState.Chase;
        //                TimelinePlayer.Instance.PlayTimeline(m_RunDir);
        //            }
        //            if (randam == 1)
        //            {
        //                Debug.Log("AA");
        //                // playerに遠距離攻撃を行う
        //                m_enemyState = EnemyState.RangedATK;
        //                TimelinePlayer.Instance.PlayTimeline(m_RangedATKDir);
        //            }
        //        }
        //    }          
        //}
    }
}