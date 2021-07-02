using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

namespace Junjun
{
    /// <summary>
    /// タイムラインを制御する静的クラス
    /// </summary>
    public static class TimeLinePlayer
    {
        /// <summary>
        /// 再生する
        /// </summary>
        /// <param name="playableDirector"></param>
        public static void PlayTimeline(PlayableDirector playableDirector)
        {
            playableDirector.Play();
        }

        /// <summary>
        /// 一時停止する
        /// </summary>
        /// <param name="playableDirector"></param>
        public static void PauseTimeline(PlayableDirector playableDirector)
        {
            playableDirector.Pause();
        }

        /// <summary>
        /// 一時停止を再開する
        /// </summary>
        /// <param name="playableDirector"></param>
        public static void ResumeTimeline(PlayableDirector playableDirector)
        {
            playableDirector.Resume();
        }

        /// <summary>
        /// 停止する
        /// </summary>
        /// <param name="playableDirector"></param>
        public static void StopTimeline(PlayableDirector playableDirector)
        {
            playableDirector.Stop();
        }
    }

    /// <summary>
    /// ゲーム全体の管理をするマネージャークラス
    /// </summary>
    public class GameManager : SingletonMonoBehavior<GameManager>
    {
        #region Scene Name
        /// <summary>タイトルシーンの名前</summary>
        [SerializeField] public string m_title = "Title";
        /// <summary>バトルシーン名前</summary>
        [SerializeField] public string m_battle = "Test_Goblins";
        /// <summary>ゲームクリアシーンの名前</summary>
        [SerializeField] public string m_gameClear = "GameClear";
        /// <summary>ゲームオーバーシーンの名前</summary>
        [SerializeField] public string m_gameOver = "GameOver";
        #endregion

        #region Game UI
        /// <summary>タイマー表示用テキスト</summary>
        public GameObject m_timerText;
        /// <summary>最速タイムを表示するテキスト</summary>
        [SerializeField] public GameObject m_bestTimeText;
        /// <summary>Playerのメニューウィンドウ</summary>
        GameObject m_menuWindow;
        /// <summary>タイトルに戻るボタン/summary>
        GameObject m_returnTitleButton;
        /// <summary>Gameを始めるボタン</summary>
        GameObject m_gameStartButton;
        #endregion

        #region In Game Time
        /// <summary>タイマーが止まる時間</summary>
        [SerializeField] public float m_stopTimer;
        /// <summary>経過時間（分）</summary>
        [SerializeField, HideInInspector] public int m_minute;
        /// <summary>経過時間（秒）</summary>
        [SerializeField, HideInInspector] public float m_seconds;
        /// <summary>前のUpdateの時の秒数</summary>
        [SerializeField, HideInInspector] public float m_oldSeconds;
        #endregion

        #region GameState
        StateMachine<GameManager> stateMachine;

        private IState<GameManager> titleState = new Title();
        public IState<GameManager> TitleState { get => titleState; }

        private IState<GameManager> inGameState = new InGame();
        public IState<GameManager> InGameState { get => inGameState; }

        private IState<GameManager> gameClearState = new GameClear();
        public IState<GameManager> GameClearState { get => gameClearState; }

        private IState<GameManager> gameOverState = new GameOver();
        public IState<GameManager> GameOverState { get => gameOverState; }

        bool m_isInGame = false;
        #endregion



        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            if (stateMachine == null)
            {
                stateMachine = new StateMachine<GameManager>(this, TitleState);
            }
        }

        private void Start()
        {
            if (stateMachine.currentState == TitleState)
            {
                stateMachine.currentState.OnExecute(this);
            }
        }

        private void Update()
        {
            if (m_isInGame)
            {
                if (stateMachine.currentState == InGameState)
                {
                    Debug.Log(stateMachine.currentState);
                   
                    StartCoroutine("StopWatch");
                    m_isInGame = false;
                    
                }
            }
        }

        IEnumerator StopWatch()
        {
            while (true)
            {
                stateMachine.currentState.OnExecute(this);
                yield return null;
            }
        }

        /// <summary>
        /// タイトルシーンに遷移
        /// </summary>
        public async void ChangeTitleScene()
        {
            Debug.Log(stateMachine);
            m_isInGame = false;
            stateMachine.currentState = TitleState;
            SceneLoader.Instance.Load(m_title);
            await UniTask.Delay(TimeSpan.FromSeconds(3.0f));
            m_bestTimeText = GameObject.Find("BestTime");
            stateMachine.currentState.OnExecute(this);
            m_gameStartButton = GameObject.Find("GameStart");
            m_gameStartButton.GetComponent<Button>().onClick.AddListener(() => ChangeGameScene());
        }

        /// <summary>
        /// ゲームシーンに遷移
        /// </summary>
        public async void ChangeGameScene()
        {
            stateMachine.ChageMachine(InGameState);
            TimeInit();
            SceneLoader.Instance.Load(m_battle);
            await UniTask.Delay(TimeSpan.FromSeconds(3.0f));
            m_timerText = GameObject.Find("TimeText");
            m_returnTitleButton = GameObject.Find("ReturnTitle");
            m_returnTitleButton.GetComponent<Button>().onClick.AddListener(() => ChangeTitleScene());
            m_menuWindow = GameObject.Find("MenuWindow");
            m_menuWindow.gameObject.SetActive(false);
            m_isInGame = true;

            
        }

        /// <summary>
        /// ゲームクリア
        /// </summary>
        public async void GameClear()
        {
            m_isInGame = false;
            stateMachine.ChageMachine(GameClearState);
            SaveAndLoad.Instance.SaveTimeData(m_minute, m_seconds);
            SceneLoader.Instance.Load(m_gameClear);
            await UniTask.Delay(TimeSpan.FromSeconds(3.0f));
            m_returnTitleButton = GameObject.Find("ReturnTitle");
            m_returnTitleButton.GetComponent<Button>().onClick.AddListener(() => ChangeTitleScene());
        }

        public async void GameOver()
        {
            m_isInGame = false;
            stateMachine.ChageMachine(GameOverState);
            SceneLoader.Instance.Load(m_gameOver);
            await UniTask.Delay(TimeSpan.FromSeconds(3.0f));
            m_returnTitleButton = GameObject.Find("ReturnTitle");
            m_returnTitleButton.GetComponent<Button>().onClick.AddListener(() => ChangeTitleScene());
        }

        /// <summary>
        /// Timeを初期化する関数
        /// </summary>
        void TimeInit()
        {
            m_minute = 0;
            m_seconds = 0;
            m_oldSeconds = 0;
        }
    }
}