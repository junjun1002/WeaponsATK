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
    /// �^�C�����C���𐧌䂷��ÓI�N���X
    /// </summary>
    public static class TimeLinePlayer
    {
        /// <summary>
        /// �Đ�����
        /// </summary>
        /// <param name="playableDirector"></param>
        public static void PlayTimeline(PlayableDirector playableDirector)
        {
            playableDirector.Play();
        }

        /// <summary>
        /// �ꎞ��~����
        /// </summary>
        /// <param name="playableDirector"></param>
        public static void PauseTimeline(PlayableDirector playableDirector)
        {
            playableDirector.Pause();
        }

        /// <summary>
        /// �ꎞ��~���ĊJ����
        /// </summary>
        /// <param name="playableDirector"></param>
        public static void ResumeTimeline(PlayableDirector playableDirector)
        {
            playableDirector.Resume();
        }

        /// <summary>
        /// ��~����
        /// </summary>
        /// <param name="playableDirector"></param>
        public static void StopTimeline(PlayableDirector playableDirector)
        {
            playableDirector.Stop();
        }
    }

    /// <summary>
    /// �Q�[���S�̂̊Ǘ�������}�l�[�W���[�N���X
    /// </summary>
    public class GameManager : SingletonMonoBehavior<GameManager>
    {
        #region Scene Name
        /// <summary>�^�C�g���V�[���̖��O</summary>
        [SerializeField] public string m_title = "Title";
        /// <summary>�o�g���V�[�����O</summary>
        [SerializeField] public string m_battle = "Test_Goblins";
        /// <summary>�Q�[���N���A�V�[���̖��O</summary>
        [SerializeField] public string m_gameClear = "GameClear";
        /// <summary>�Q�[���I�[�o�[�V�[���̖��O</summary>
        [SerializeField] public string m_gameOver = "GameOver";
        #endregion

        #region Game UI
        /// <summary>�^�C�}�[�\���p�e�L�X�g</summary>
        public GameObject m_timerText;
        /// <summary>�ő��^�C����\������e�L�X�g</summary>
        [SerializeField] public GameObject m_bestTimeText;
        /// <summary>Player�̃��j���[�E�B���h�E</summary>
        GameObject m_menuWindow;
        /// <summary>�^�C�g���ɖ߂�{�^��/summary>
        GameObject m_returnTitleButton;
        /// <summary>Game���n�߂�{�^��</summary>
        GameObject m_gameStartButton;
        #endregion

        #region In Game Time
        /// <summary>�^�C�}�[���~�܂鎞��</summary>
        [SerializeField] public float m_stopTimer;
        /// <summary>�o�ߎ��ԁi���j</summary>
        [SerializeField, HideInInspector] public int m_minute;
        /// <summary>�o�ߎ��ԁi�b�j</summary>
        [SerializeField, HideInInspector] public float m_seconds;
        /// <summary>�O��Update�̎��̕b��</summary>
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
        /// �^�C�g���V�[���ɑJ��
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
        /// �Q�[���V�[���ɑJ��
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
        /// �Q�[���N���A
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
        /// Time������������֐�
        /// </summary>
        void TimeInit()
        {
            m_minute = 0;
            m_seconds = 0;
            m_oldSeconds = 0;
        }
    }
}