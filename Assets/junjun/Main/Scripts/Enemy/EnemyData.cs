using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    [Serializable, CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string name;

        public int hp;

        public int power;
    }
}

