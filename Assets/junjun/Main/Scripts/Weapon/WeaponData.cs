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
        [SerializeField, Header("•Ší‚Ì–¼‘O")] public string weaponName;
        [SerializeField, Header("•Ší‚ÌÅ¬UŒ‚—Í")]public int minAtk;
        [SerializeField, Header("•Ší‚ÌÅ‚UŒ‚—Í")] public int maxAtk;

    }
}
