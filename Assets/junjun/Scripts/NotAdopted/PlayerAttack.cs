using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Playerの装備している武器の種類
    [SerializeField] public ProfessionType m_professionType;
    // 拳を握ってるかどうか
    public bool m_holdFist = false;
    // Start is called before the first frame update
    void Start()
    {
        m_professionType = ProfessionType.Gladiator;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_professionType == ProfessionType.Gladiator)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
                m_holdFist = true;
            }
        }
    }
}
/// <summary>
/// Playerの職業
/// 持つ武器で変わる
/// 後々魔法使いとかも作りたい
/// </summary>
public enum ProfessionType
{
    Gladiator, Warrior,Gunner
}