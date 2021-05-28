using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerを制御するクラス
/// </summary>
public class VRPlayerController : MonoBehaviour
{
    ///<summary> SP</summary>
    [SerializeField] public int m_sp;
    
   
    /// <summary>HP</summary>
    public float m_playerHp = 1f;
    /// <summary>スローモーションの効果時間</summary>
    [SerializeField] float m_zoneTime;


    private void Update()
    {
        if (m_playerHp <= 0)
        {
            GameManager.Instance.GameOver();
            Debug.Log("GameOver");
        }

        if (OVRInput.Get(OVRInput.Button.Start))
        {
            UIManager.Instance.ActiveUI();
        }

        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            if (m_sp >= 30)
            {
                UIManager.Instance.UseSPUI(0.3f);
                m_sp -= 30;
                Debug.Log("Zoneだどん");
                TimeState.Instance.SlowTime();
                Invoke("StopZone", m_zoneTime);
            }
        }
    }

    /// <summary>
    /// スローモーション状態を止める
    /// </summary>
    void StopZone()
    {
        TimeState.Instance.RestoredTime();
    }
}
