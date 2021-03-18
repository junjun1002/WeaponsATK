using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scene上のUIを
/// </summary>
public class UIManager : SingletonMonoBehavior<UIManager>
{
    [SerializeField] ImgsFillDynamic m_playerHPUI;
    [SerializeField] ImgsFillDynamic m_playerSPUI;
    [SerializeField] float m_healValue;
    [SerializeField] GameObject m_menuWindow;
    public float span = 5f;

    [SerializeField] float m_stopTimer;
    //　タイマー表示用テキスト
    [SerializeField] Text m_timerText;

    private int m_minute;
    private float m_seconds;
    //　前のUpdateの時の秒数
    private float m_oldSeconds;


    private float m_maxSP;
    private float m_currentSP;
    private bool m_activeUI;
    protected override void Awake()
    {
        base.Awake();

    }

    private void Start()
    {
        m_maxSP = m_playerSPUI.TargetValue;
        m_currentSP = m_playerSPUI.TargetValue;
        StartCoroutine("StopWatch");
        StartCoroutine("Logging");
    }

    IEnumerator StopWatch()
    {
        while (true)
        {
            if (m_minute < m_stopTimer)
            {
                m_seconds += Time.unscaledDeltaTime;
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

    IEnumerator Logging()
    {
        while (true)
        {
            yield return new WaitForSeconds(span);
            Debug.LogFormat("{0}秒経過", span);
            NaturalRecoverySPUI();
        }
    }

    /// <summary>
    /// ダメージを受けた時によばれる関数
    /// </summary>
    /// <param name="damage"></param>
    public void DecreasesHPUI(float damage)
    {
        m_playerHPUI.SetValue(damage, false, Random.Range(0.01f, 0.5f));
    }

    public void NaturalRecoverySPUI()
    {
        if (!(m_currentSP == m_maxSP))
        {
            m_playerSPUI.SetValue(m_healValue, false, Random.Range(0.01f, 0.5f));
        }
    }

    /// <summary>
    /// SPを消費した時に呼ばれる関数
    /// </summary>
    /// <param name="useSP"></param>
    public void UseSPUI(float useSP)
    {
        m_playerSPUI.SetValue(useSP, false, Random.Range(0.01f, 0.5f));
        m_currentSP -= useSP;
    }

    public void ActiveUI()
    {
        if (m_activeUI)
        {
            m_menuWindow.SetActive(false);
        }
        else
        {
            m_menuWindow.SetActive(true);
        }

        if (m_activeUI)
        {
            m_activeUI = false;
        }
        else
        {
            m_activeUI = true;
        }
    }
}
