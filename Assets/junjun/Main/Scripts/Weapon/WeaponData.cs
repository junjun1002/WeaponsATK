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
        [SerializeField, Header("����̎���ID")] WeaponID weaponID;
        [SerializeField, Header("����̖��O")] string weaponName;
        [SerializeField, Header("����̍U����")] int weaponPower;

        /// <summary>����̎���ID</summary>
        public WeaponID WeaponID { get => weaponID; }
        /// <summary>����̖��O</summary>
        public string WeaponName { get => weaponName; }
        /// <summary>����̍U����</summary>
        public int WeaponPower { get => weaponPower; }

    }

    /// <summary>
    /// ����̎���ID
    /// </summary>
    public enum WeaponID
    {
        LongSword, ShortSword
    }
}
