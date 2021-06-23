using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    /// <summary>
    /// ����̃��X�g���쐬����ScritableObject
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "WeaponList", menuName = "List/WeaponList")]
    public class WeaponList : ScriptableObject
    {
        [SerializeField] List<WeaponData> weaposDatas;

        public List<WeaponData> WeaponDatas { get => weaposDatas; }
    }
}
