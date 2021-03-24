using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerÇêßå‰Ç∑ÇÈÉNÉâÉX
public class VRPlayerController : MonoBehaviour
{
    // SP
    [SerializeField] public int m_sp;
   

    public float m_playerHp = 1f;

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
                Debug.Log("ZoneÇæÇ«ÇÒ");
                TimeState.Instance.SlowTime();
                Invoke("StopZone", m_zoneTime);
            }
        }
    }

    void StopZone()
    {
        TimeState.Instance.RestoredTime();
    }
}
