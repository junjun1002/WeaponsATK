using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    /// <summary>
    /// •Ší‚ÌƒŠƒXƒg‚ğì¬‚·‚éScritableObject
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "EnemyList", menuName = "List/EnemyList")]
    public class EnemyList : ScriptableObject
    {
        EnemyStatusData[] enemyDatas;
        public EnemyStatusData[] EnemyDatas { get => enemyDatas; }
    }
}
