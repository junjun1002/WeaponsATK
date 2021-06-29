using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

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
        /// <summary>タイトルシーンの名前</summary>
        [SerializeField] public string m_title = "Title";
        /// <summary>バトルシーン名前</summary>
        [SerializeField] public string m_battle = "Test_Goblins";
        /// <summary>ゲームクリアシーンの名前</summary>
        [SerializeField] public string m_gameClear = "GameClear";
        /// <summary>ゲームオーバーシーンの名前</summary>
        [SerializeField] public string m_gameOver = "GameOver";

        /// <summary>タイマーが止まる時間</summary>
        [SerializeField] public float m_stopTimer;
        /// <summary>タイマー表示用テキスト</summary>
        public GameObject m_timerText;
        /// <summary>最速タイムを表示するテキスト</summary>
        [SerializeField] public Text m_bestTimeText;
        /// <summary>Playerのメニューウィンドウ</summary>
        GameObject m_menuWindow;

        /// <summary>経過時間（分）</summary>
        public int m_minute;
        /// <summary>経過時間（秒）</summary>
        public float m_seconds;
        /// <summary>前のUpdateの時の秒数</summary>
        public float m_oldSeconds;

        StateMachine<GameManager> stateMachine;

        private IState<GameManager> titleState = new Title();
        public IState<GameManager> TitleState { get => titleState; }

        private IState<GameManager> inGameState = new InGame(); 
        public IState<GameManager> InGameState { get => inGameState; }

        private IState<GameManager> gameClearState = new GameClear();
        public IState<GameManager> GameClearState { get => gameClearState; }

        private IState<GameManager> gameOverState = new GameOver();
        public IState<GameManager> GameOverState { get => gameOverState; }



        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            if (stateMachine == null)
            {
                stateMachine = new StateMachine<GameManager>(this, InGameState);
            }
        }

        private void Start()
        {
            if (stateMachine.currentState == TitleState)
            {
                stateMachine.currentState.OnExecute(this);
            }
            if (stateMachine.currentState == InGameState)
            {
                m_timerText = GameObject.Find("TimeText");
                m_menuWindow = GameObject.Find("MenuWindow");
                m_menuWindow.gameObject.SetActive(false);
                StartCoroutine("StopWatch");
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
        public void ChangeTitleScene()
        {
            stateMachine.ChageMachine(TitleState);
            SceneLoader.Instance.Load(m_title);
        }

        /// <summary>
        /// ゲームシーンに遷移
        /// </summary>
        public void ChangeGameScene()
        {
            stateMachine.ChageMachine(GameClearState);
           
            SceneLoader.Instance.Load(m_battle);
        }

        /// <summary>
        /// ゲームクリア
        /// </summary>
        public void GameClear()
        {
            stateMachine.ChageMachine(GameClearState);
            SaveAndLoad.Instance.SaveTimeData(m_minute, m_seconds);
            SceneLoader.Instance.Load(m_gameClear);
        }

        public void GameOver()
        {
            stateMachine.ChageMachine(GameOverState);
            SceneLoader.Instance.Load(m_gameOver);
        }
    }

    public class Title : IState<GameManager>
    {
        public void OnExecute(GameManager owner)
        {
            SaveAndLoad.Instance.LoadTimeData(owner.m_bestTimeText);
        }
    }

    public class InGame : IState<GameManager>
    {
        public void OnExecute(GameManager owner)
        {
            if (owner.m_minute < owner.m_stopTimer)
            {
                owner.m_seconds += Time.unscaledDeltaTime;
                if (owner.m_seconds >= 60f)
                {
                    owner.m_minute++;
                    owner.m_seconds = owner.m_seconds - 60;
                }
                //　値が変わった時だけテキストUIを更新
                if (owner.m_seconds != owner.m_oldSeconds)
                {
                    owner.m_timerText.GetComponent<Text>().text = owner.m_minute.ToString() + ":" + owner.m_seconds.ToString("f1");
                }
                owner.m_oldSeconds = owner.m_seconds;
            }
        }
    }

    public class GameClear : IState<GameManager>
    {
        public void OnExecute(GameManager owner)
        {

        }
    }

    public class GameOver : IState<GameManager>
    {
        public void OnExecute(GameManager owner)
        {
            
        }
    }
}