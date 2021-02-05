using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeaponEvent : MonoBehaviour, IEventCollision
{
    [SerializeField] WeaponsType m_weaponsType;

    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        // テスト用
        gameObject.SetActive(false);
        eventSystem.ExecuteGetWeaponEvent(m_weaponsType);
       
    }
}

/// <summary>
/// 武器の種類
/// 後々杖を追加したい
/// </summary>
public enum WeaponsType
{
    Sword, Gun
}
