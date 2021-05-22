using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : StateBase
{
    public EnemyStateType m_enemyState;

    public override string GetStateName()
    {
        return m_enemyState.ToString();
    }
}
