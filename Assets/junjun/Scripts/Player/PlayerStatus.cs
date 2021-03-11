using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // SP
    [SerializeField] public float m_sp;

    public float m_playerHp = 1f;
    [SerializeField] ImgsFillDynamic imgsFillDynamic;

    private void Start()
    {
        //m_hp = imgsFillDynamic.TargetValue;
    }
    private void Update()
    {
        if (m_playerHp <= 0)
        {
            GameState.Instance.GameOver();
            Debug.Log("GameOver");
        }
    }
}
