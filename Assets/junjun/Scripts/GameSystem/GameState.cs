using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム状態を管理するクラス
/// </summary>
public class GameState : SingletonMonoBehavior<GameState>
{
    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }
    protected bool InGame { get; set; }

    public void GameOver()
    {

    }

    public void GameClear()
    {

    }

    
}
