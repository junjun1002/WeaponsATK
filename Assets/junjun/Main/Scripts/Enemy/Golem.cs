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
            case EnemyStateType.None:
                break;
            case EnemyStateType.Idle:
                break;
            case EnemyStateType.Chase:
                break;
            case EnemyStateType.Attack:
                break;
            case EnemyStateType.RangedATK:
                break;
            case EnemyStateType.CoolTime:
                break;
            default:
                break;
        }
    }
}
