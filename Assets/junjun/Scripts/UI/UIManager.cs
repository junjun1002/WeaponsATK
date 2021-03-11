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
    public float span = 5f;


    private float m_maxSP;
    private float m_currentSP;
    protected override void Awake()
    {
        base.Awake();
      
    }

    private void Start()
    {
        m_maxSP = m_playerSPUI.TargetValue;
        m_currentSP = m_playerSPUI.TargetValue;
        StartCoroutine("Logging");
    }

    IEnumerator Logging()
    {
        while (true)
        {
            yield return new WaitForSeconds(span);
            Debug.LogFormat("{0}秒経過", span);
            NaturalRecoverySP();
        }
    }
    /// <summary>
    /// ダメージを受けた時によばれる関数
    /// </summary>
    /// <param name="damage"></param>
    public void DecreasesHP(float damage)
    {
        m_playerHPUI.SetValue(damage, false, Random.Range(0.01f, 0.5f));
    }

    public void NaturalRecoverySP()
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
    public void UseSP(float useSP)
    {
        m_playerSPUI.SetValue(useSP, false, Random.Range(0.01f, 0.5f));
        m_currentSP -= useSP;
    }
}
