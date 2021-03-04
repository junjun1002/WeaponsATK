using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIのテストをするクラス
/// </summary>
public class TestUI : MonoBehaviour
{
    [SerializeField] float m_useSP = 0.1f;

        public void TestDamadeCause()
    {
        float DamageValue = Random.Range(0.02f, 0.1f);
        UIManager.Instance.DecreasesHP(DamageValue);
        Debug.Log(DamageValue * 100 + "ダメージを与えた");
    }

    public void TestUseSP()
    {
        UIManager.Instance.UseSP(m_useSP);
        Debug.Log("SPを" + m_useSP * 100 + "消費した");
    }
}
