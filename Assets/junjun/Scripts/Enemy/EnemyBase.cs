using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public EnemyState m_enemyState;

    protected float m_distance;

    protected Rigidbody m_rb;
    protected Tweener m_enemyDOMove;

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
        m_enemyDOMove = transform.DOLocalMove(new Vector3(m_player.transform.position.x, 0f, m_player.transform.position.z), m_speed);
    }

    /// <summary>
    /// 自分の動きを止める
    /// </summary>
    protected void MoveStop()
    {
        m_enemyDOMove = transform.DOLocalMove(new Vector3(transform.position.x, 0f, transform.position.z), 0.1f);
        m_enemyDOMove.Play();
    }

    /// <summary>
    /// 状態をidleにチェンジ
    /// </summary>
    protected void EnemyStateIdle()
    {
        Debug.Log("idle");
        m_enemyState = EnemyState.Idle;
    }
}

/// <summary>
/// Enemyの状態を表すenum
/// </summary>
public enum EnemyState
{
    None, Idle, Chase, Attack, RangedATK
}

/// <summary>
/// Enemyの種類を表すEnum
/// </summary>
public enum EnemyType
{
    PunchingBag, Spider, Tiger
}
