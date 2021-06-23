using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Junjun
{
    /// <summary>
    /// @•Šíˆê‚Âˆê‚Â‚Ìİ’è
    /// </summary>
    [Serializable, CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField, Header("•Ší‚Ì¯•ÊID")] WeaponID weaponID;
        [SerializeField, Header("•Ší‚Ì–¼‘O")] string weaponName;
        [SerializeField, Header("•Ší‚ÌUŒ‚—Í")] int weaponPower;

        /// <summary>•Ší‚Ì¯•ÊID</summary>
        public WeaponID WeaponID { get => weaponID; }
        /// <summary>•Ší‚Ì–¼‘O</summary>
        public string WeaponName { get => weaponName; }
        /// <summary>•Ší‚ÌUŒ‚—Í</summary>
        public int WeaponPower { get => weaponPower; }

    }

    /// <summary>
    /// •Ší‚Ì¯•ÊID
    /// </summary>
    public enum WeaponID
    {
        LongSword, ShortSword
    }
}
