using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemInGameScene : MonoBehaviour
{
    public event Action<int, EnemyType> EnemyEvent;
    //public event Action<WeaponsType> GetWeaponEvent;

    public void ExecuteEnemyEvent(int score,EnemyType enemyType)
    {
        if (enemyType == EnemyType.PunchingBag)
        {
            Debug.Log("私はサンドバッグ");
            Debug.Log("もっと殴ってぇ～～～～///");
        }
        else
        {
            EnemyEvent?.Invoke(score, enemyType);
            Debug.Log(enemyType + "を倒した");
            Debug.Log(score + "を手に入れた");
        }
    }

    //public void ExecuteGetWeaponEvent(WeaponsType weaponsType)
    //{
    //    GetWeaponEvent?.Invoke(weaponsType);
    //    Debug.Log(weaponsType + "を装備した");
    //}
}
