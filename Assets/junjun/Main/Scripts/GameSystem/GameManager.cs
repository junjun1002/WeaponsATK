using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Junjun;

/// <summary>
/// �Q�[���S�̂̊Ǘ�������}�l�[�W���[�N���X
/// </summary>
public class GameManager : SingletonMonoBehavior<GameManager>
{
    /// <summary>�^�C�g���V�[���̖��O</summary>
    [SerializeField] string m_title = "Title";
    /// <summary>�o�g���V�[�����O</summary>
    [SerializeField] string m_battle = "TigerBattle";
    /// <summary>�Q�[���N���A�V�[���̖��O</summary>
    [SerializeField] string m_gameClear = "GameClear";
    /// <summary>�Q�[���I�[�o�[�V�[���̖��O</summary>
    [SerializeField] string m_gameOver = "GameOver";

    /// <summary>�^�C�}�[���~�܂鎞��</summary>
    [SerializeField] float m_stopTimer;
    /// <summary>�^�C�}�[�\���p�e�L�X�g</summary>
    [SerializeField] Text m_timerText;

    /// <summary>�o�ߎ��ԁi���j</summary>
    private int m_minute;
    /// <summary>�o�ߎ��ԁi�b�j</summary>
    private float m_seconds;
    /// <summary>�O��Update�̎��̕b��</summary>
    private float m_oldSeconds;

    /// <summary>�Q�[���̏��</summary>
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
                    //�@�l���ς�����������e�L�X�gUI���X�V
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
    /// �^�C�g���V�[���ɑJ��
    /// </summary>
    public void ChangeTitleScene()
    {
        SceneLoader.Instance.Load(m_title);
    }

    /// <summary>
    /// �Q�[���V�[���ɑJ��
    /// </summary>
    public void ChangeGameScene()
    {
        SceneLoader.Instance.Load(m_battle);
    }

    /// <summary>
    /// �Q�[���N���A
    /// </summary>
    public void GameClear()
    {
        m_gameState = GameState.GameClear;
        SceneLoader.Instance.Load(m_gameClear);
    }

    /// <summary>
    /// �Q�[���I�[�o�[
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
/// �Q�[���̏�Ԃ�\��enum
/// </summary>
enum GameState
{
    Title, InGame, GameClear, GameOver
}