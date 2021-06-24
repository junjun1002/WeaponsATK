using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    /// <summary>
    /// �@������̐ݒ�
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "EnemyStatusData", menuName = "Data/EnemyStatusData")]
    public class EnemyStatusData : ScriptableObject
    {
        [SerializeField, Header("�G�̖��O")] string enemyName;
        [SerializeField, Header("�G��HP")] int enemyMaxHP;
        [SerializeField, Header("�G�̍U����")] int enemyPower;

        /// <summary>�G�̖��O</summary>
        public string EnemyName { get => enemyName; }
        /// <summary>�G��HP</summary>
        public int EnemyHP { get => enemyMaxHP; }
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
