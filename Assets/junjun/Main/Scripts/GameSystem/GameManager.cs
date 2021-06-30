using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

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
        [SerializeField] public Text m_bestTimeText;
        /// <summary>Player�̃��j���[�E�B���h�E</summary>
        GameObject m_menuWindow;
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
                m_timerText = GameObject.Find("TimeText");
                m_menuWindow = GameObject.Find("MenuWindow");
                m_menuWindow.gameObject.SetActive(false);
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
        public void ChangeTitleScene()
        {
            Debug.Log(stateMachine);
            m_isInGame = false;
            stateMachine.currentState = TitleState;
            SceneLoader.Instance.Load(m_title);
        }

        /// <summary>
        /// �Q�[���V�[���ɑJ��
        /// </summary>
        public void ChangeGameScene()
        {
            stateMachine.ChageMachine(InGameState);
            SceneLoader.Instance.Load(m_battle);
            m_isInGame = true;
        }

        /// <summary>
        /// �Q�[���N���A
        /// </summary>
        public void GameClear()
        {
            m_isInGame = false;
            stateMachine.ChageMachine(GameClearState);
            SaveAndLoad.Instance.SaveTimeData(m_minute, m_seconds);
            SceneLoader.Instance.Load(m_gameClear);
        }

        public void GameOver()
        {
            m_isInGame = false;
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
                //�@�l���ς�����������e�L�X�gUI���X�V
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