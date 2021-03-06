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
        if (m_readyToATK)
        {
            // playerとの距離が攻撃範囲よりも遠い時
            if (m_distance >= m_atkRange)
            {
                if (m_enemyState == EnemyState.Chase)
                {
                    if (m_run)
                    {
                        MoveToPlayer();
                    }
                }
                int randam = Random.Range(0, 2);
                Debug.Log(randam);
                if (randam == 0)
                {
                    // playerを追いかける状態に入る
                    m_enemyState = EnemyState.Chase;
                    TimelinePlayer.Instance.PlayTimeline(m_RunDir);
                }
                if (randam == 1)
                {
                    Debug.Log("AA");
                    // playerに遠距離攻撃を行う
                    m_enemyState = EnemyState.RangedATK;
                    TimelinePlayer.Instance.PlayTimeline(m_RangedATKDir);
                }
            }
            // playerが攻撃範囲内にいるとき
            if (m_distance <= m_atkRange)
            {
                m_run = false;
                m_enemyState = EnemyState.Idle;
            }
           
        }
    }
}