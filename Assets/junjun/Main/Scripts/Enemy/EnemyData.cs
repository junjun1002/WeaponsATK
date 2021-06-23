using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    /// <summary>
    /// �@������̐ݒ�
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField, Header("�G�̎���ID")] EnemyID enemyID;
        [SerializeField, Header("�G�̖��O")] string enemyName;
        [SerializeField, Header("�G��HP")] int enemyHP;
        [SerializeField, Header("�G�̍U����")] int enemyPower;

        /// <summary>�G�̎���ID</summary>
        public EnemyID EnemyID { get => enemyID; }
        /// <summary>�G�̖��O</summary>
        public string EnemyName { get => enemyName; }
        /// <summary>�G��HP</summary>
        public int EnemyHP { get => enemyHP; }
        /// <summary>�G�̍U����</summary>
        public int EnemyPower { get => enemyPower; }

    }

    /// <summary>
    /// ����̎���ID
    /// </summary>
    public enum EnemyID
    {
        LongSword, ShortSword
    }
}
