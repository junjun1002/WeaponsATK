using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : EnemyBase
{

    [SerializeField] GameObject m_rangedATKEffect;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (m_enemyState == EnemyState.RangedATK)
        {
            TimelinePlayer.Instance.PlayTimeline(TimelinePlayer.Instance.playableDirector[0]);
        } 
    }
}