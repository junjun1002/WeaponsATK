using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Junjun
{
    /// <summary>
    /// ゲームのステート情報だけを所持しているクラス
    /// </summary>
    public class GameState : SingletonMonoBehavior<GameState>
    { 
        #region GameState
        public StateMachine<GameManager> stateMachine;

        private IState<GameManager> titleState = new Title();
        public IState<GameManager> TitleState { get => titleState; }

        private IState<GameManager> inGameState = new InGame();
        public IState<GameManager> InGameState { get => inGameState; }

        private IState<GameManager> gameClearState = new GameClear();
        public IState<GameManager> GameClearState { get => gameClearState; }

        private IState<GameManager> gameOverState = new GameOver();
        public IState<GameManager> GameOverState { get => gameOverState; }

        public bool m_isInGame;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            stateMachine = new StateMachine<GameManager>(GameManager.Instance.gameManager, TitleState);
        }
    }
}
