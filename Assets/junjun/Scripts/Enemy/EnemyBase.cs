using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    // 補完スピードを決める
    [SerializeField] public float m_lookSpeed = 0.1f;
    // HP
    [SerializeField] public int m_hp;
    // enemyの種類
    [SerializeField] protected EnemyType m_enemyType;
    // Player(ターゲット)
    [SerializeField] protected GameObject m_player;
    // 敵のアニメーション
    [SerializeField] protected Animator m_anim;
    // 敵が止まる距離
    [SerializeField] protected float m_atkRange = 20;
    // 攻撃力
    float m_atkPoint;
    // enemyの状態
    public EnemyState m_enemyState;
    public NavMeshAgent m_agent;

    protected float m_distance;
    public PlayerStatus playerStatus;



    virtual protected void Start()
    {
        m_enemyState = EnemyState.Idle;
        m_anim = GetComponent<Animator>();
        m_atkPoint = Random.Range(0.05f, 0.08f);
    }

    virtual protected void Update()
    {
        // playerと自分の距離を測る
        m_distance = Vector3.Distance(transform.position, m_player.transform.position);
        if (m_hp <= 0)
        {
            GameState.Instance.GameClear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("くらえ");
            m_atkPoint = Random.Range(0.05f, 0.08f);
            UIManager.Instance.DecreasesHPUI(m_atkPoint);
            playerStatus.m_playerHp -= m_atkPoint;
        }
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

    protected void LookAtPlayer()
    {
        // ターゲット方向のベクトルを取得
        Vector3 relativePos = m_player.transform.position - this.transform.position;
        // 方向を、回転情報に変換
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        // 現在の回転情報と、ターゲット方向の回転情報を補完する
        transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, m_lookSpeed);
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
