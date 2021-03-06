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
    [SerializeField] float m_atkRange = 20;
    // enemyの状態
    protected EnemyState m_enemyState;
    // 攻撃準備ができてるか
    protected bool m_readyToATK = true;

    protected Rigidbody m_rb;


    int m_diff;
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
        float distance = Vector3.Distance(transform.position, m_player.transform.position);
        // playerとの距離が攻撃範囲よりも遠い時
        if (distance >= m_atkRange)
        {
            // playerを追いかける状態の時
            if (m_enemyState == EnemyState.Chase)
            {
                Debug.Log("いくお");
                MoveToPlayer();
            }
            // 殺意湧きました。どうしてやろうか
            if (m_anim)
            {
                m_anim.SetBool("killing", true);
                int randam = Random.Range(0, 2);
                m_anim.SetInteger("randam", randam);
                Debug.Log(randam);
                if (randam == 0)
                {
                    // playerを追いかける状態に入る
                    m_enemyState = EnemyState.Chase;
                }
                if (randam == 1)
                {
                    // playerに遠距離攻撃を行う
                    m_enemyState = EnemyState.RangedATK;
                }
            }
           
        }
        // playerが攻撃範囲内にいるとき
        if (distance <= m_atkRange)
        {
            if (m_anim)
            {
                m_anim.SetBool("killing", false);
                m_enemyState = EnemyState.Idle;
            }
        }
    }
    
    /// <summary>
    /// playerを追いかける
    /// </summary>
    void MoveToPlayer()
    {
        Vector3 velocity = gameObject.transform.rotation * new Vector3(0, 0, m_speed);
        gameObject.transform.position += velocity * Time.deltaTime;
    }
    protected void ReadyInversion()
    {
        if (m_readyToATK)
        {
            m_readyToATK = false;
        }
        else
        {
            m_readyToATK = true;
        }
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
