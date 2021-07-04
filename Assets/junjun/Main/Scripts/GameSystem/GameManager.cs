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
        public Text m_timerText;
        /// <summary>最速タイムを表示するテキスト</summary>
        [SerializeField] public Text m_bestTimeText;
        /// <summary>Playerのメニューウィンドウ</summary>
        [SerializeField] GameObject m_menuWindow;
        /// <summary>タイトルに戻るボタン/summary>
        [SerializeField] GameObject m_returnTitleButton;
        /// <summary>Gameを始めるボタン</summary>
        [SerializeField] GameObject m_gameStartButton;
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

        /// <summary>FindをさせないためにSerializeしとく</summary>
        [SerializeField] public GameManager gameManager;

        GameState gameState;
        StateMachine<GameManager> stateMachine;

        private bool m_isTimeLoad = true;

        [SerializeField] GameObject m_enemy;

        protected override void Awake()
        {
            base.Awake();
            gameState = GameState.Instance;
        }

        private void Start()
        {
            stateMachine = gameState.stateMachine;

            if (stateMachine.currentState == gameState.InGameState)
            {
                StartCoroutine("StopWatch");
            }

        }

        private void Update()
        {

            if (m_isTimeLoad)
            {
                if (m_bestTimeText)
                {
                    if (stateMachine.currentState == gameState.TitleState)
                    {
                        stateMachine.currentState.OnExecute(this);
                        m_isTimeLoad = false;
                    }
                }
            }
        }

        /// <summary>
        /// 戦闘時間を計る非同期処理
        /// </summary>
        /// <returns></returns>
        IEnumerator StopWatch()
        {
            while (true)
            {
                if (stateMachine.currentState == gameState.TitleState)
                {
                    StopCoroutine("StopWatch");
                    yield return null;
                }
                stateMachine.currentState.OnExecute(this);
                yield return null;
            }
        }

        /// <summary>
        /// タイトルシーンに遷移
        /// </summary>
        public void ChangeTitleScene()
        {
            if (stateMachine.currentState == gameState.InGameState)
            {
                m_enemy.SetActive(false);
            }
            stateMachine.ChageMachine(gameState.TitleState);
            SceneLoader.Instance.Load(m_title);
        }

        /// <summary>
        /// ゲームシーンに遷移
        /// </summary>
        public void ChangeGameScene()
        {
            stateMachine.ChageMachine(gameState.InGameState);
            SceneLoader.Instance.Load(m_battle);
        }

        /// <summary>
        /// ゲームクリア
        /// </summary>
        public void GameClear()
        {
            stateMachine.ChageMachine(gameState.GameClearState);
            SaveAndLoad.Instance.SaveTimeData(m_minute, m_seconds);
            SceneLoader.Instance.Load(m_gameClear);
        }

        public void GameOver()
        {
            stateMachine.ChageMachine(gameState.GameOverState);
            SceneLoader.Instance.Load(m_gameOver);
        }
    }
}