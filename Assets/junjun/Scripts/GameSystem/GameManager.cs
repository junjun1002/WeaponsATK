using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] string m_title = "Title";
    [SerializeField] string m_battle = "Test_TigerBattle";
    [SerializeField] string m_gameClear = "GameClear";
    [SerializeField] string m_gameOver = "GameOver";

    [SerializeField] float m_stopTimer;
    //　タイマー表示用テキスト
    [SerializeField] Text m_timerText;

    private int m_minute;
    private float m_seconds;
    //　前のUpdateの時の秒数
    private float m_oldSeconds;

    [SerializeField] GameState m_gameState;
    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(this);
    }

    private void Update()
    {
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

    public void ChangeTitleScene()
    {
        SceneLoder.Instance.Load(m_title);
    }

    public void ChangeGameScene()
    {
        SceneLoder.Instance.Load(m_battle);
    }
    public void GameClear()
    {
        m_gameState = GameState.GameClear;
        SceneLoder.Instance.Load(m_gameClear);
    }

    public void GameOver()
    {
        m_gameState = GameState.GameOver;
        SceneLoder.Instance.Load(m_gameOver);
    }
}

enum GameState
{
    Title, InGame, GameClear, GameOver
}