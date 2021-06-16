using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Junjun;

/// <summary>
/// ゲーム全体の管理をするマネージャークラス
/// </summary>
public class GameManager : SingletonMonoBehavior<GameManager>
{
    /// <summary>タイトルシーンの名前</summary>
    [SerializeField] string m_title = "Title";
    /// <summary>バトルシーン名前</summary>
    [SerializeField] string m_battle = "TigerBattle";
    /// <summary>ゲームクリアシーンの名前</summary>
    [SerializeField] string m_gameClear = "GameClear";
    /// <summary>ゲームオーバーシーンの名前</summary>
    [SerializeField] string m_gameOver = "GameOver";

    /// <summary>タイマーが止まる時間</summary>
    [SerializeField] float m_stopTimer;
    /// <summary>タイマー表示用テキスト</summary>
    [SerializeField] Text m_timerText;

    /// <summary>経過時間（分）</summary>
    private int m_minute;
    /// <summary>経過時間（秒）</summary>
    private float m_seconds;
    /// <summary>前のUpdateの時の秒数</summary>
    private float m_oldSeconds;

    /// <summary>ゲームの状態</summary>
    [SerializeField] GameState m_gameState;

    StateMachine<GameManager> stateMachine;

    public IState<GameManager> TitleState { get; set; } = new Title();

    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(this);
        stateMachine = new StateMachine<GameManager>(this, TitleState);
    }


    private void Update()
    {
        stateMachine.currentState.OnExecute(this);
        switch (m_gameState)
        {
            case GameState.Title:
                break;
            case GameState.InGame:
                if (m_minute < m_stopTimer)
                {
                    m_seconds += Time.unscaledDeltaTime;
                    if (m_seconds >= 60f)
                    {
                        m_minute++;
                        m_seconds = m_seconds - 60;
                    }
                    //　値が変わった時だけテキストUIを更新
                    if (m_seconds != m_oldSeconds)
                    {
                        m_timerText.text = m_minute.ToString() + ":" + m_seconds.ToString("f1");
                    }
                    m_oldSeconds = m_seconds;
                }
                break;
            case GameState.GameClear:
                SaveAndLoad.Instance.SaveTimeData(m_minute, m_seconds);
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// タイトルシーンに遷移
    /// </summary>
    public void ChangeTitleScene()
    {
        SceneLoader.Instance.Load(m_title);
    }

    /// <summary>
    /// ゲームシーンに遷移
    /// </summary>
    public void ChangeGameScene()
    {
        SceneLoader.Instance.Load(m_battle);
    }

    /// <summary>
    /// ゲームクリア
    /// </summary>
    public void GameClear()
    {
        m_gameState = GameState.GameClear;
        SceneLoader.Instance.Load(m_gameClear);
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver()
    {
        m_gameState = GameState.GameOver;
        SceneLoader.Instance.Load(m_gameOver);
    }
}

public class Title : IState<GameManager>
{
    public void OnExecute(GameManager owner)
    {
        
    }
    public void A() { }
    public void B() { }

}


/// <summary>
/// ゲームの状態を表すenum
/// </summary>
enum GameState
{
    Title, InGame, GameClear, GameOver
}