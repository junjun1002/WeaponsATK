using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    // enemyの種類
    [SerializeField] EnemyType m_enemyType;
    // 攻撃後のクールタイム
    [SerializeField] float m_coolTime;
    // 移動速度
    [SerializeField] float m_speed = 1.0f;
    // Player
    [SerializeField] GameObject m_player;
    // 敵のアニメーション
    [SerializeField] Animator m_anim;
    // 敵が止まる距離
    [SerializeField] float m_stopDistance = 20;
    // enemyの状態
    EnemyState m_enemyState;

    Rigidbody m_rb;
    

    int m_diff;
    private void Start()
    {
        m_enemyState = EnemyState.Idle;
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        m_enemyState = EnemyState.Chase;
        transform.LookAt(m_player.transform);
        // playerを追いかける状態の時
        if (m_enemyState == EnemyState.Chase)
        {
            float distance = Vector3.Distance(transform.position, m_player.transform.position);
            if (distance >= m_stopDistance)
            {
                Debug.Log("いくお");
                MoveToPlayer();
            }
            if (distance <= m_stopDistance)
            {
                if (m_anim)
                {
                    m_anim.SetBool("chase", false);
                }
            }
        }
    }

    void MoveToPlayer()
    {
        Vector3 velocity = gameObject.transform.rotation * new Vector3(0, 0, m_speed);
        gameObject.transform.position += velocity * Time.deltaTime;
        if (m_anim)
        {
            m_anim.SetBool("chase", true);
        }
    }
}

/// <summary>
/// Enemyの状態を表すenum
/// </summary>
public enum EnemyState
{
    Idle, Chase, Attack, CoolTime
}

/// <summary>
/// Enemyの種類を表すEnum
/// </summary>
public enum EnemyType
{
    PunchingBag, Spider, Tiger
}
