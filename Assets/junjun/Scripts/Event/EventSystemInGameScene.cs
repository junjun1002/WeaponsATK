using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemInGameScene : MonoBehaviour
{
    public event Action<EnemyType> EnemyEvent;
    public event Action GameOverEvent;
    public event Action GameClearEvent;

    public void ExecuteEnemyEvent(EnemyType enemyType)
    {
        if (enemyType == EnemyType.PunchingBag)
        {
            Debug.Log("私はサンドバッグ");
            Debug.Log("もっと殴ってぇ～～～～///");
        }
        else
        {
            EnemyEvent?.Invoke(enemyType);
            Debug.Log(enemyType + "を倒した");
            ExecuteGameClear();
        }
    }

    public void ExecuteGameOver()
    {
        GameOverEvent?.Invoke();
        GameState.Instance.GameOver();
    }

    public void ExecuteGameClear()
    {
        GameClearEvent?.Invoke();
        GameState.Instance.GameClear();
    }
}
