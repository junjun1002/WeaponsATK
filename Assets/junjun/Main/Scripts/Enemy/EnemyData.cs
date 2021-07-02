using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    [Serializable, CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField, Header("“G‚Ì–¼‘O")] public string enemyName;
        [SerializeField, Header("“G‚ÌHP")] public int hp;
        [SerializeField, Header("“G‚ÌUŒ‚—Í")] public int power;
    }
}

