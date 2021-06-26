using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    /// <summary>
    /// ����̃��X�g���쐬����ScritableObject
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "EnemyList", menuName = "List/EnemyList")]
    public class EnemyList : ScriptableObject
    {
        EnemyData[] enemyDatas;
        public EnemyData[] EnemyDatas { get => enemyDatas; }
    }
}
