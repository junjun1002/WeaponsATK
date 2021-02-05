﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemInGameScene : MonoBehaviour
{
    public event Action<int, EnemyType> AttackEnemyEvent;
    public event Action<WeaponsType> GetWeaponEvent;

    public void ExecuteAttackEnemyEvent(int score,EnemyType enemyType)
    {
        AttackEnemyEvent?.Invoke(score, enemyType);     
    }

    public void ExecuteGetWeaponEvent(WeaponsType weaponsType)
    {
        Debug.Log("武器だよ！");
        GetWeaponEvent?.Invoke(weaponsType);
        Debug.Log(weaponsType + "を装備した");
    }
}
