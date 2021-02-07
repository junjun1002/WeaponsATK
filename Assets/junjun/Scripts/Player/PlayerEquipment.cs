using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipment : MonoBehaviour
{
    //  今装備しているもの
    [SerializeField] public WeaponsType m_weaponsType;
    [SerializeField] Button m_noneButton;
    [SerializeField] Button m_swordButton;
    [SerializeField] Button m_gunButton;

    Dictionary<int, WeaponsType> weaponsDictionary = new Dictionary<int, WeaponsType>();
    // Start is called before the first frame update
    void Start()
    {
        m_weaponsType = WeaponsType.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/// <summary>
/// Playerの装備している武器の種類
/// 後々杖を追加したい（素手で魔法出そうかな）
/// </summary>
public enum WeaponsType
{
    None, Sword, Gun
}

