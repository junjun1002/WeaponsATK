using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    /// <summary>
    /// @•Šíˆê‚Âˆê‚Â‚Ìİ’è
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField, Header("“G‚Ì¯•ÊID")] EnemyID enemyID;
        [SerializeField, Header("“G‚Ì–¼‘O")] string enemyName;
        [SerializeField, Header("“G‚ÌHP")] int enemyHP;
        [SerializeField, Header("“G‚ÌUŒ‚—Í")] int enemyPower;

        /// <summary>“G‚Ì¯•ÊID</summary>
        public EnemyID EnemyID { get => enemyID; }
        /// <summary>“G‚Ì–¼‘O</summary>
        public string EnemyName { get => enemyName; }
        /// <summary>“G‚ÌHP</summary>
        public int EnemyHP { get => enemyHP; }
        /// <summary>“G‚ÌUŒ‚—Í</summary>
        public int EnemyPower { get => enemyPower; }

    }

    /// <summary>
    /// •Ší‚Ì¯•ÊID
    /// </summary>
    public enum EnemyID
    {
        LongSword, ShortSword
    }
}
