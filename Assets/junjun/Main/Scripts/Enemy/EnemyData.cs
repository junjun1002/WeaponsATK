using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    [Serializable, CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField, Header("�G�̖��O")] public string enemyName;
        [SerializeField, Header("�G��HP")] public int hp;
        [SerializeField, Header("�G�̍U����")] public int power;
    }
}

