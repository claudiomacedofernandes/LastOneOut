using UnityEngine;

namespace LastOneOut
{
    public class BoardManager : MonoBehaviour
    {
        public GameObject board = null;

        private void OnEnable()
        {
            GameManager.instance.onGameStateChange += OnGameStateChangeHandler;
        }

        private void OnDisable()
        {
            GameManager.instance.onGameStateChange -= OnGameStateChangeHandler;
        }

        public void OnGameStateChangeHandler(GameState newGameState, GameInfo gameInfo = null)
        {
            switch (newGameState)
            {
                case GameState.NONE:
                case GameState.MENU:
                case GameState.EXIT:
                    HideBoard();
                    break;
                case GameState.NEW_GAME:
                    Init();
                    break;
                case GameState.RUNNING:
                    ShowBoard();
                    break;
                case GameState.ENDED:
                    break;
            }
        }

        public void Init()
        {
            GameManager.instance.SetBoardManagerReady(true);
        }

        public void ShowBoard()
        {
            board.SetActive(true);
        }

        public void HideBoard()
        {
            board.SetActive(false);
        }
    }
}
