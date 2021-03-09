using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    // 武器を装備する位置
    [SerializeField] GameObject m_handPos;
    // 剣のオブジェクト
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


