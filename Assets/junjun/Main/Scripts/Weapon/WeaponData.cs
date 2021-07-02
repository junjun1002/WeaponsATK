using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    /// <summary>
    /// �@������̐ݒ�
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField, Header("����̖��O")] public string weaponName;
        [SerializeField, Header("����̍ŏ��U����")]public int minAtk;
        [SerializeField, Header("����̍ō��U����")] public int maxAtk;

    }
}
