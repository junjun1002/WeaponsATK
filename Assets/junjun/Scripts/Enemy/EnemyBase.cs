using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    // enemyの種類
    [SerializeField] protected EnemyType m_enemyType;
    // 移動速度
    [SerializeField] float m_speed = 1.0f;
    // Player
    [SerializeField] protected GameObject m_player;
    // 敵のアニメーション
    [SerializeField] protected Animator m_anim;
    // 敵が止まる距離
    [SerializeField] protected float m_atkRange = 20;
    // enemyの状態
    protected EnemyState m_enemyState;
    // 攻撃準備ができてるか
    protected bool m_readyToATK = true;

    protected float m_distance;
    protected bool m_run = false;

    protected Rigidbody m_rb;

    virtual protected void Start()
    {
        m_enemyState = EnemyState.Idle;
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }

    virtual protected void Update()
    {
        // 常にplayerの方向を向く
        transform.LookAt(m_player.transform);
        // playerと自分の距離を測る
        m_distance = Vector3.Distance(transform.position, m_player.transform.position);      
    }

    /// <summary>
    /// playerを追いかける
    /// </summary>
    protected void MoveToPlayer()
    {
        Debug.Log("よっしゃ走るで");
        Vector3 velocity = gameObject.transform.rotation * new Vector3(0, 0, m_speed);
        gameObject.transform.position += velocity * Time.deltaTime;
    }
    protected void ReadyInversion()
    {
        Debug.Log("反転するお");
        if (m_readyToATK)
        {
            m_readyToATK = false;
        }
        else
        {
            m_readyToATK = true;
        }
    }

    protected void ReadyRun()
    {
        m_run = true;
    }

    protected void RunStop()
    {
        m_run = false;
    }
}

/// <summary>
/// Enemyの状態を表すenum
/// </summary>
public enum EnemyState
{
    Idle, Chase, Attack, RangedATK, CoolTime
}

/// <summary>
/// Enemyの種類を表すEnum
/// </summary>
public enum EnemyType
{
    PunchingBag, Spider, Tiger
}
