using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEvent : MonoBehaviour, IEventCollision
{
    //  武器の種類
    [SerializeField] public WeaponsType m_weaponsType;

    public void CollisionEvent(EventSystemInGameScene eventSystem)
    {
        throw new System.NotImplementedException();
    }
}


