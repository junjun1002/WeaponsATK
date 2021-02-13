using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    [SerializeField] Text m_text;
    [SerializeField] float m_stopTimer;

    public float m_timer;
    public bool timerState;

    private void Update()
    {
        if (timerState)
        {
            if ()
            {

            }
            m_timer += Time.deltaTime;
            m_text.text = m_timer.ToString();
        }
    }

    public void StopWatchState()
    {
        timerState = true;
    }
}
