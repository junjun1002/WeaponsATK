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
        public Text m_timerText;
        /// <summary>�ő��^�C����\������e�L�X�g</summary>
        [SerializeField] public Text m_bestTimeText;
        /// <summary>Player�̃��j���[�E�B���h�E</summary>
        [SerializeField] GameObject m_menuWindow;
        /// <summary>�^�C�g���ɖ߂�{�^��/summary>
        [SerializeField] GameObject m_returnTitleButton;
        /// <summary>Game���n�߂�{�^��</summary>
        [SerializeField] GameObject m_gameStartButton;
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

        /// <summary>Find�������Ȃ����߂�Serialize���Ƃ�</summary>
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
        /// �퓬���Ԃ��v��񓯊�����
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
        /// �^�C�g���V�[���ɑJ��
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
        /// �Q�[���V�[���ɑJ��
        /// </summary>
        public void ChangeGameScene()
        {
            stateMachine.ChageMachine(gameState.InGameState);
            SceneLoader.Instance.Load(m_battle);
        }

        /// <summary>
        /// �Q�[���N���A
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