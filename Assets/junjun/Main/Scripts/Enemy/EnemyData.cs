using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    /// <summary>
    /// @íêÂêÂÌÝè
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField, Header("GÌ¯ÊID")] EnemyID enemyID;
        [SerializeField, Header("GÌ¼O")] string enemyName;
        [SerializeField, Header("GÌHP")] int enemyHP;
        [SerializeField, Header("GÌUÍ")] int enemyPower;

        /// <summary>GÌ¯ÊID</summary>
        public EnemyID EnemyID { get => enemyID; }
        /// <summary>GÌ¼O</summary>
        public string EnemyName { get => enemyName; }
        /// <summary>GÌHP</summary>
        public int EnemyHP { get => enemyHP; }
        /// <summary>GÌUÍ</summary>
        public int EnemyPower { get => enemyPower; }

    }

    /// <summary>
    /// íÌ¯ÊID
    /// </summary>
    public enum EnemyID
    {
        LongSword, ShortSword
    }
}
