using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] GameObject m_handPos;
    private WeaponsType m_weaponsType;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_weaponsType = WeaponsType.Sword;
            WeaponChange();
        }
    }
    void WeaponChange()
    {
        if (m_weaponsType == WeaponsType.Sword)
        {
            GetComponentInChildren<Sword>().GenerateWeapon();
        }
    }
}


