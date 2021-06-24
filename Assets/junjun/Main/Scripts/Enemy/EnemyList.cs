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
        [SerializeField] List<EnemyStatusData> enemyDatas;

        public List<EnemyStatusData> EnemyDatas { get => enemyDatas; }
    }
}
