using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetWeaponEvent : MonoBehaviour, IEventCollision
{
    [SerializeField] WeaponsType m_weaponsType;

    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
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
