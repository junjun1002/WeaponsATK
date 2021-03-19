using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeState : SingletonMonoBehavior<TimeState>
{
    [SerializeField] float m_slowTime;

    protected override void Awake()
    {
        base.Awake();
    }
    public void SlowTime()
    {
        Time.timeScale = m_slowTime;
        Debug.Log("ようこそスロウな世界");
    }

    public void RestoredTime()
    {
        Debug.Log("元の時間だどん");
        Time.timeScale = 1;
    }
}
