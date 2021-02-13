using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    [SerializeField] float m_stopTimer;
    //　タイマー表示用テキスト
    [SerializeField] Text m_timerText;

    public bool m_timerStart;
    private int m_minute;
    private float m_seconds;
    //　前のUpdateの時の秒数
    private float m_oldSeconds;

    void Update()
    {
        if (m_timerStart)
        {
            if (m_minute < m_stopTimer)
            {
                m_seconds += Time.deltaTime;
                if (m_seconds >= 60f)
                {
                    m_minute++;
                    m_seconds = m_seconds - 60;
                }
                //　値が変わった時だけテキストUIを更新
                if (m_seconds != m_oldSeconds)
                {
                    m_timerText.text = m_minute.ToString() + ":" + m_seconds.ToString("f1");
                }
                m_oldSeconds = m_seconds;
            }
        }
    }
    public void StopWatchState()
    {
        m_timerStart = true;
    }
}
