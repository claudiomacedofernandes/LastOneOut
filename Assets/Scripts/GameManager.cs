using UnityEngine;

namespace LastOneOut
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector] public static GameManager instance = null;
        [HideInInspector] public System.Action<GameState, GameInfo> onGameStateChange = null;
        public GameState prevGameState = GameState.NONE;
        public GameState gameState = GameState.NONE;
        private bool boardManagerReady = false;
        private bool playerManagerReady = false;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        void Start()
        {
            SetGameState(GameState.MENU);
        }

        void Quit()
        {
            Application.Quit();
        }

        public void SetGameState(GameState newGameState, GameInfo gameInfo = null)
        {
            if (newGameState == GameState.NEW_GAME)
                ResetGameReadyState();

            if (onGameStateChange != null)
                onGameStateChange(newGameState, gameInfo);

            prevGameState = gameState;
            gameState = newGameState;

            if (newGameState == GameState.NEW_GAME)
                CheckGameReadyState();
        }

        public void SetBoardManagerReady(bool newState)
        {
            boardManagerReady = newState;
            CheckGameReadyState();
        }

        public void SetPlayerManagerReady(bool newState)
        {
            playerManagerReady = newState;
            CheckGameReadyState();
        }

        public void ResetGameReadyState()
        {
            boardManagerReady = false;
            playerManagerReady = false;
        }

        public void CheckGameReadyState()
        {
            if (gameState != GameState.NEW_GAME)
                return;

            if (boardManagerReady == false)
                return;

            if (playerManagerReady == false)
                return;

            SetGameState(GameState.RUNNING);
        }

    }
}