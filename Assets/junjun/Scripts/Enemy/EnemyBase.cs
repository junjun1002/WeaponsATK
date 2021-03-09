using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    // enemyの種類
    [SerializeField] protected EnemyType m_enemyType;
    // 移動速度
    [SerializeField] float m_speed = 1.0f;
    // Player(ターゲット)
    [SerializeField] protected GameObject m_player;
    // 敵のアニメーション
    [SerializeField] protected Animator m_anim;
    // 敵が止まる距離
    [SerializeField] protected float m_atkRange = 20;
    // enemyの状態
    public EnemyState m_enemyState;
    public NavMeshAgent m_agent;

    protected float m_distance;

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
        m_agent.SetDestination(m_player.transform.position);
        if (m_enemyState == EnemyState.Chase)
        {
            m_agent.isStopped = false;
        }
    }

    /// <summary>
    /// 自分の動きを止める
    /// </summary>
    protected void MoveStop()
    {
        Debug.Log("止まるドン");
        m_agent.isStopped = true;
    }

    /// <summary>
    /// 状態をidleにチェンジ
    /// </summary>
    protected void EnemyStateIdle()
    {
        Debug.Log("idle");
        m_enemyState = EnemyState.Idle;
    }

    /// <summary>
    /// 状態をCoolTimeにする
    /// </summary>
    protected void EnemyStateCoolTime()
    {     
        m_enemyState = EnemyState.CoolTime;
        Debug.Log(m_enemyState);
    }
}

/// <summary>
/// Enemyの状態を表すenum
/// </summary>
public enum EnemyState
{
    None, Idle, Chase, Attack, RangedATK, CoolTime
}

/// <summary>
/// Enemyの種類を表すEnum
/// </summary>
public enum EnemyType
{
    PunchingBag, Spider, Tiger
}
