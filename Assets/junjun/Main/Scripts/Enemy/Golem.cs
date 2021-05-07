using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : EnemyBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        switch (m_enemyState)
        {
            case EnemyState.None:
                break;
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.RangedATK:
                break;
            case EnemyState.CoolTime:
                break;
            default:
                break;
        }
    }
}
