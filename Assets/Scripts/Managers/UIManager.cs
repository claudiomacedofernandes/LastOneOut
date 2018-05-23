using System;
using UnityEngine;

namespace LastOneOut
{
    public class UIManager : MonoBehaviour
    {
        public GameObject homeCanvas = null;
        public GameObject setupCanvas = null;
        public GameObject gameCanvas = null;
        public GameObject endCanvas = null;
        public GameObject menuCanvas = null;

        private void OnEnable()
        {
            GameManager.instance.onGameStateChange += OnGameStateChangeHandler;
            GameManager.instance.onGameTurnChange += OnGameTurnChangeHandler;
        }

        private void OnDisable()
        {
            GameManager.instance.onGameStateChange -= OnGameStateChangeHandler;
            GameManager.instance.onGameTurnChange -= OnGameTurnChangeHandler;
        }

        public void SetGameMenu(bool isActive)
        {
            menuCanvas.SetActive(isActive);
        }

        public void SetNewGame(GameInfo gameInfo)
        {
            GameManager.instance.StartNewGame(gameInfo);
        }

        public void StartNewGame(PlayerIndex playerIndex)
        {
            GameManager.instance.SetGameState(GameState.RUN);
            GameManager.instance.StartTurn(playerIndex);
        }

        public void EndPlayerRun()
        {
            GameManager.instance.StartTurn();
        }

        public void OnGameStateChangeHandler(object stateInfo = null)
        {
            SetGameMenu(false);
            homeCanvas.SetActive(GameManager.instance.gameState == GameState.MENU);
            setupCanvas.SetActive(GameManager.instance.gameState == GameState.SETUP);
            gameCanvas.SetActive(GameManager.instance.gameState == GameState.RUN);
            endCanvas.SetActive(GameManager.instance.gameState == GameState.END);
        }

        private void OnGameTurnChangeHandler()
        {

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
            SetNewGame(new GameInfo(PlayerType.HUMAN, PlayerType.HUMAN));
        }

        public void OnButtonHumanVSNimatron()
        {
            SetNewGame(new GameInfo(PlayerType.HUMAN, PlayerType.NIMATRON));
        }

        public void OnButtonNimatronVSNimatron()
        {
            SetNewGame(new GameInfo(PlayerType.NIMATRON, PlayerType.NIMATRON));
        }

        public void OnButtonPlayerOneStarts()
        {
            StartNewGame(PlayerIndex.PLAYER_ONE);
        }

        public void OnButtonPlayerTwoStarts()
        {
            StartNewGame(PlayerIndex.PLAYER_TWO);
        }

        public void OnButtonEndTurn()
        {
            EndPlayerRun();
        }
    }
}