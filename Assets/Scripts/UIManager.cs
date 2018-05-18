using UnityEngine;

namespace LastOneOut
{
    public class UIManager : MonoBehaviour
    {
        public GameObject homeCanvas = null;
        public GameObject gameCanvas = null;
        public GameObject menuCanvas = null;

        private void OnEnable()
        {
            GameManager.instance.onGameStateChange += OnGameStateChangeHandler;
        }

        private void OnDisable()
        {
            GameManager.instance.onGameStateChange -= OnGameStateChangeHandler;
        }

        public void StartNewGame(GameInfo gameInfo)
        {
            GameManager.instance.SetGameState(GameState.NEW_GAME, gameInfo);
        }

        public void SetGameMenu(bool isActive)
        {
            menuCanvas.SetActive(isActive);
        }

        public void OnGameStateChangeHandler(GameState newGameState, GameInfo gameInfo = null)
        {
            SetGameMenu(false);
            homeCanvas.SetActive(newGameState == GameState.MENU);
            gameCanvas.SetActive(newGameState == GameState.RUNNING);
        }

        public void OnButtonExit()
        {
            GameManager.instance.SetGameState(GameState.EXIT);
        }

        public void OnButtonHome()
        {
            GameManager.instance.SetGameState(GameState.MENU);
        }

        public void OnButtonOpenGameMenu()
        {
            SetGameMenu(true);
        }

        public void OnButtonCloseGameMenu()
        {
            SetGameMenu(false);
        }

        public void OnButtonHumanVSHuman()
        {
            StartNewGame(new GameInfo(PlayerType.HUMAN, PlayerType.HUMAN));
        }

        public void OnButtonHumanVSNimatron()
        {
            StartNewGame(new GameInfo(PlayerType.HUMAN, PlayerType.NIMATRON));
        }

        public void OnButtonNimatronVSNimatron()
        {
            StartNewGame(new GameInfo(PlayerType.NIMATRON, PlayerType.NIMATRON));
        }
    }
}