using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    /// <summary>
    /// 武器のリストを作成するScritableObject
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "EnemyList", menuName = "List/EnemyList")]
    public class EnemyList : ScriptableObject
    {
        [SerializeField] List<EnemyData> enemyDatas;

        public List<EnemyData> EnemyDatas { get => enemyDatas; }
    }
}
