using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    [Serializable, CreateAssetMenu(fileName = "EnemyStatusData", menuName = "Data/EnemyStatusData")]
    public class EnemyStatusData : ScriptableObject
    {
        public EnemyData[] enemyDatas;

        [SerializeField, Header("“G‚Ì–¼‘O")] string enemyName;
        [SerializeField, Header("“G‚ÌHP")] int enemyMaxHP;
        [SerializeField, Header("“G‚ÌUŒ‚—Í")] int enemyPower;

        /// <summary>“G‚Ì–¼‘O</summary>
        public string EnemyName { get => enemyName; }
        /// <summary>“G‚ÌHP</summary>
        public int EnemyHP { get => enemyMaxHP; }
        /// <summary>“G‚ÌUŒ‚—Í</summary>
        public int EnemyPower { get => enemyPower; }

        public void SetCsv()
        {
            
        }
    }
}
