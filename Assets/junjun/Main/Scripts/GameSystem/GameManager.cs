using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

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
        [SerializeField] public string m_battle = "TigerBattle";
        /// <summary>ゲームクリアシーンの名前</summary>
        [SerializeField] public string m_gameClear = "GameClear";
        /// <summary>ゲームオーバーシーンの名前</summary>
        [SerializeField] public string m_gameOver = "GameOver";

        /// <summary>タイマーが止まる時間</summary>
        [SerializeField] public float m_stopTimer;
        /// <summary>タイマー表示用テキスト</summary>
        [SerializeField] public Text m_timerText;
        /// <summary>最速タイムを表示するテキスト</summary>
        [SerializeField] public Text m_bestTimeText;

        /// <summary>経過時間（分）</summary>
        public int m_minute;
        /// <summary>経過時間（秒）</summary>
        public float m_seconds;
        /// <summary>前のUpdateの時の秒数</summary>
        public float m_oldSeconds;

        StateMachine<GameManager> stateMachine;

        public IState<GameManager> TitleState { get; set; } = new Title();

        public IState<GameManager> InGameState { get; set; } = new InGame();

        protected override void Awake()
        {
            base.Awake();
            //DontDestroyOnLoad(this);
            stateMachine = new StateMachine<GameManager>(this, InGameState);
        }

        private void Start()
        {
            if (stateMachine.currentState == TitleState)
            {
                stateMachine.currentState.OnExecute(this);
            }
            if (stateMachine.currentState == InGameState)
            {
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
            stateMachine.currentState = TitleState;
            SceneLoader.Instance.Load(m_title);
        }

        /// <summary>
        /// ゲームシーンに遷移
        /// </summary>
        public void ChangeGameScene()
        {
            stateMachine.currentState = TitleState;
            SceneLoader.Instance.Load(m_battle);
        }

        /// <summary>
        /// ゲームクリア
        /// </summary>
        public async void GameClear()
        {
            SaveAndLoad.Instance.SaveTimeData(m_minute, m_seconds);
            SceneLoader.Instance.Load(m_gameClear);
        }

        public void GameOver()
        {
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
                    owner.m_timerText.text = owner.m_minute.ToString() + ":" + owner.m_seconds.ToString("f1");
                }
                owner.m_oldSeconds = owner.m_seconds;
            }
        }
    }
}