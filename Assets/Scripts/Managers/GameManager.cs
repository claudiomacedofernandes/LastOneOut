using UnityEngine;

namespace LastOneOut
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector] public static GameManager instance = null;
        [HideInInspector] public System.Action<GameState, object> onGameStateChange = null;
        [HideInInspector] public System.Action onGameTurnChange = null;
        [HideInInspector] public System.Action<BoardItem> onGameItemSelected = null;
        public GameState prevGameState = GameState.NONE;
        public GameState gameState = GameState.NONE;
        public GameData currentGameData = null;
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

        public void StartNewGame(GameInfo gameInfo)
        {
            currentGameData = new GameData();
            SetGameState(GameState.NEW_GAME, gameInfo);
        }

        public void StartTurn(PlayerIndex playerIndex = PlayerIndex.NONE)
        {
            if (playerIndex == PlayerIndex.NONE)
                currentGameData.currentPlayerIndex = currentGameData.currentPlayerIndex == PlayerIndex.PLAYER_ONE ? PlayerIndex.PLAYER_TWO : PlayerIndex.PLAYER_ONE;
            else
                currentGameData.currentPlayerIndex = playerIndex;

            if (onGameTurnChange != null)
                onGameTurnChange();
        }

        public void SetGameState(GameState newGameState, object stateInfo = null)
        {
            if (newGameState == GameState.NEW_GAME)
                ResetGameReadyState();

            if (onGameStateChange != null)
                onGameStateChange(newGameState, stateInfo);

            prevGameState = gameState;
            gameState = newGameState;

            if (newGameState == GameState.NEW_GAME)
                CheckGameReadyState();

            Debug.Log("SetGameState: " + gameState);
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

            SetGameState(GameState.SETUP);
        }

        public void SelectBoardItem(BoardItem selectedItem)
        {
            if (onGameItemSelected != null)
            {
                onGameItemSelected(selectedItem);
            }

            CheckGameEnd();
        }

        public void CheckGameEnd()
        {
            if (currentGameData.boardItems.Count == 0)
            {
                IPlayer winner = currentGameData.currentPlayerIndex == PlayerIndex.PLAYER_ONE ? currentGameData.playerTwo : currentGameData.playerOne;
                string winnerString = winner == currentGameData.playerOne ? "playerOne" : "playerTwo";
                Debug.Log("Game Ended");
                Debug.Log("Winner Is: " + winnerString);

                SetGameState(GameState.END);
            }
        }
    }
}