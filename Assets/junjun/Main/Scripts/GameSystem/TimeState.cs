using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 時間を管理するクラス
/// </summary>
public class TimeState : SingletonMonoBehavior<TimeState>
{
    /// <summary>タイムスケールの値</summary>
    [SerializeField] float m_slowTime;

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// スローモーションになる関数
    /// </summary>
    public void SlowTime()
    {
        Time.timeScale = m_slowTime;
        Debug.Log("ようこそスロウな世界");
    }

    /// <summary>
    /// 元の時間に戻す関数
    /// </summary>
    public void RestoredTime()
    {
        Debug.Log("元の時間だどん");
        Time.timeScale = 1;
    }
}
