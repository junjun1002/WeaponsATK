using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] GameObject m_handPos;
    [SerializeField] GameObject m_sword;
    private WeaponsType m_weaponsType;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_weaponsType = WeaponsType.None;
            m_sword.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_weaponsType = WeaponsType.Sword;
            WeaponChange();
        }  
    }
    /// <summary>
    /// 該当するオブジェクトを手の位置に移動のちアクティブ化
    /// </summary>
    void WeaponChange()
    {
        if (m_weaponsType == WeaponsType.Sword)
        {
            m_sword.transform.position = m_handPos.transform.position;
            m_sword.GetComponent<Sword>().GenerateWeapon();
        }
    }
}


