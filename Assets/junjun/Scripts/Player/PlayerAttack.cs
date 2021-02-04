using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Playerの装備している武器の種類
    [SerializeField] WeaponsType m_weaponsType;
    // 拳を握ってるかどうか
    public bool m_holdFist = false;
    // Start is called before the first frame update
    void Start()
    {
        m_weaponsType = WeaponsType.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_weaponsType == WeaponsType.None)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
                m_holdFist = true;
            }
        }
    }
}


/// <summary>
/// Playerが持ってる武器の種類
/// （後々杖とか追加して魔法とか追加するかも）
/// </summary>
public enum WeaponsType
{
    None, Sword, Gun
}