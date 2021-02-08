using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponsBase : MonoBehaviour
{
    [SerializeField] WeaponsType m_weaponsType;

    private void Start()
    {
        gameObject.SetActive(false);
    }
}

/// <summary>
/// 武器の種類
/// 後々杖を追加したい（素手で魔法出そうかな）
/// </summary>
public enum WeaponsType
{
    None, Sword, Gun
}