using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

/// <summary>
/// Enemyの基底クラス
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
    ///<summary>補完スピードを決める</summary> 
    [SerializeField] public float m_lookSpeed = 0.1f;
    ///<summar>HP</summar> 
    [SerializeField] public int m_hp;
    ///<summary>enemyの種類</summary> 
    [SerializeField] protected EnemyType m_enemyType;
    ///<summary> Player(ターゲット)</summary>
    [SerializeField] protected GameObject m_player;
    ///<summary> 敵のアニメーション</summary>
    [SerializeField] public Animator m_anim;
    ///<summary> 敵が止まる距離</summary>
    [SerializeField] protected float m_atkRange = 20;
    ///<summary> 攻撃力</summary>
    float m_atkPoint;
    ///<summary> enemyの状態</summary>
    public EnemyState m_enemyState;
    /// <summary> 無敵状態の判定</summary>
    private bool m_Invincible;

    /// <summary>PlayerとEnemyの距離 </summary>
    protected float m_distance;
    public NavMeshAgent m_agent;

    public VRPlayerController m_vrPlayercontroller;
    public SkinnedMeshRenderer m_meshRenderer;

    /// <summary>ノックバックする力</summary>
    private Vector3 m_knockBackVelocity = Vector3.zero;
    /// <summary>ノックバックする力</summary>
    [SerializeField] protected float m_knockBackPower;
    virtual protected void Start()
    {
        m_enemyState = EnemyState.Idle;
        m_anim = GetComponent<Animator>();
        m_atkPoint = UnityEngine.Random.Range(0.05f, 0.08f);
    }

    virtual protected void Update()
    {
        // playerと自分の距離を測る
        m_distance = Vector3.Distance(transform.position, m_player.transform.position);
        if (m_hp <= 0)
        {
            GameManager.Instance.GameClear();
        }
        // ノックバックする
        if (m_knockBackVelocity != Vector3.zero)
        {
            m_agent.Move(m_knockBackVelocity * Time.deltaTime);
        }
    }

    /// <summary>
    /// Plyerにダメージを与えるときの処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("くらえ");
            m_atkPoint = UnityEngine.Random.Range(0.05f, 0.08f);
            UIManager.Instance.DecreasesHPUI(m_atkPoint);
            m_vrPlayercontroller.m_playerHp -= m_atkPoint;
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

    /// <summary>
    /// 滑らかにPlayerの方向を向くように
    /// </summary>
    protected void LookAtPlayer()
    {
        // ターゲット方向のベクトルを取得
        Vector3 relativePos = m_player.transform.position - this.transform.position;
        // 方向を、回転情報に変換
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        // 現在の回転情報と、ターゲット方向の回転情報を補完する
        transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, m_lookSpeed);
    }
    
    /// <summary>
    /// ダメージを受けた時にノックバックする
    /// </summary>
    public async void KnockBack()
    {
        /// 多段ヒットしないように攻撃を受けて少しの間は無敵化
        if (m_Invincible)
        {
            return;
        }
        m_Invincible = true;
        Debug.Log("ノックバック");
        m_knockBackVelocity = -transform.forward * m_knockBackPower;
        m_meshRenderer.material.color = Color.red;
        m_enemyState = EnemyState.KnockBack;
        m_anim.SetBool("Hit", true);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        m_knockBackVelocity = Vector3.zero;
        m_meshRenderer.material.color = Color.white;
        m_enemyState = EnemyState.Idle;
        m_Invincible = false;
    }
}

/// <summary>
/// Enemyの状態を表すenum
/// </summary>
public enum EnemyState
{
    None, Idle, Chase, Attack, RangedATK, CoolTime, KnockBack
}

/// <summary>
/// Enemyの種類を表すEnum
/// </summary>
public enum EnemyType
{
    PunchingBag, Spider, Golem, Goblins, Tiger
}
