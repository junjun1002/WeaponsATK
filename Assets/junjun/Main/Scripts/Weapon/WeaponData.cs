using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    /// <summary>
    /// @íêÂêÂÌÝè
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField, Header("íÌ¯ÊID")] WeaponID weaponID;
        [SerializeField, Header("íÌ¼O")] string weaponName;
        [SerializeField, Header("íÌUÍ")] int weaponPower;

        /// <summary>íÌ¯ÊID</summary>
        public WeaponID WeaponID { get => weaponID; }
        /// <summary>íÌ¼O</summary>
        public string WeaponName { get => weaponName; }
        /// <summary>íÌUÍ</summary>
        public int WeaponPower { get => weaponPower; }

    }

    /// <summary>
    /// íÌ¯ÊID
    /// </summary>
    public enum WeaponID
    {
        LongSword, ShortSword
    }
}
