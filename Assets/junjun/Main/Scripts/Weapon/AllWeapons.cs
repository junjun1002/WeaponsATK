using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    public class AllWeapons : SingletonMonoBehavior<AllWeapons>
    {
        /// <summary>•Ší‚ÌƒŠƒXƒg</summary>
        [SerializeField] WeaponList weaponList;

        public WeaponData GetWeaponData(WeaponID weaponID)
        {
            var weapons = weaponList.WeaponDatas.Find(s => s.WeaponID == weaponID);
            return weapons;
        }
    }
}