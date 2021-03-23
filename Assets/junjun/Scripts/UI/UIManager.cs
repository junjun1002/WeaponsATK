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
    [SerializeField] GameObject m_uIPointer;
    [SerializeField] int m_healValue;
    [SerializeField] GameObject m_menuWindow;
    public float m_span = 5f;

    public VRPlayerController vRPlayerController;

    private float m_maxSP;
    private float m_currentSP;
    private bool m_activeUI;
    protected override void Awake()
    {
        base.Awake();

    }

    private void Start()
    {
        m_uIPointer.SetActive(false);
        m_maxSP = m_playerSPUI.TargetValue;
        m_currentSP = m_playerSPUI.TargetValue;
        StartCoroutine("Logging");
    }

    IEnumerator Logging()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_span);
            Debug.LogFormat("{0}秒経過", m_span);
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
            vRPlayerController.m_sp -= m_healValue;
            m_playerSPUI.SetValue((float)m_healValue / 100, false, Random.Range(0.01f, 0.5f));
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
            m_uIPointer.SetActive(false);
            m_menuWindow.SetActive(false);
        }
        else
        {
            m_uIPointer.SetActive(true);
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
