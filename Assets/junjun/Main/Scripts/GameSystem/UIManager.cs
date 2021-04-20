using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scene上のUIを管理するクラス
/// </summary>
public class UIManager : SingletonMonoBehavior<UIManager>
{
    /// <summary>HPのUI</summary>
    [SerializeField] ImgsFillDynamic m_playerHPUI;
    /// <summary>SPのUI</summary>
    [SerializeField] ImgsFillDynamic m_playerSPUI;
    /// <summary>VR空間内でUIを選択する用のポインター</summary>
    [SerializeField] GameObject m_uIPointer;
    /// <summary>SPが回復する値</summary>
    [SerializeField] int m_healValue;
    /// <summary>メニューのUI</summary>
    [SerializeField] GameObject m_menuWindow;
    /// <summary>SPが自然回復する間隔 </summary>
    public float m_span = 5f;

    public VRPlayerController vRPlayerController;

    /// <summary>最大SP量</summary>
    private float m_maxSP;
    /// <summary>現在のSP</summary>
    private float m_currentSP;
    /// <summary>UIがアクティブかどうかの判定</summary>
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

    /// <summary>
    /// SPの自然回復を非同期で行う
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Enemyにダメージを与えたときの処理
    /// </summary>
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

    /// <summary>
    /// UIアクティブ化を制御するクラス
    /// </summary>
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
