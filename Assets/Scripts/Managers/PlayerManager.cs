using System;
using UnityEngine;

namespace LastOneOut
{
    public class PlayerManager : MonoBehaviour
    {
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

        public void OnGameStateChangeHandler(object stateInfo = null)
        {
            switch (GameManager.instance.gameState)
            {
                case GameState.NONE:
                    break;
                case GameState.MENU:
                    break;
                case GameState.NEW_GAME:
                    OnGameStart((GameInfo)stateInfo);
                    break;
                case GameState.SETUP:
                    break;
                case GameState.RUN:
                    break;
                case GameState.END:
                    OnGameEnd();
                    break;
                case GameState.EXIT:
                    break;
            }
        }

        public void OnGameStart(GameInfo gameInfo)
        {
            GameManager.instance.currentGameData.playerOne = CreatePlayer(gameInfo.playerOneType);
            GameManager.instance.currentGameData.playerTwo = CreatePlayer(gameInfo.playerTwoType);
            GameManager.instance.SetPlayerManagerReady(true);
        }

        public void OnGameEnd()
        {
            if (GameManager.instance.currentGameData.currentPlayer != null)
            {
                GameManager.instance.currentGameData.currentPlayer.EndTurn();
                GameManager.instance.currentGameData.currentPlayer.OnItemSelected -= OnItemSelectedHandler;
            }
        }

        private void OnGameTurnChangeHandler()
        {
            if (GameManager.instance.currentGameData.currentPlayer != null)
            {
                GameManager.instance.currentGameData.currentPlayer.EndTurn();
                GameManager.instance.currentGameData.currentPlayer.OnItemSelected -= OnItemSelectedHandler;
            }

            switch (GameManager.instance.currentGameData.currentPlayerIndex)
            {
                case PlayerIndex.NONE:
                    GameManager.instance.currentGameData.currentPlayer = null;
                    break;
                case PlayerIndex.PLAYER_ONE:
                    GameManager.instance.currentGameData.currentPlayer = GameManager.instance.currentGameData.playerOne;
                    break;
                case PlayerIndex.PLAYER_TWO:
                    GameManager.instance.currentGameData.currentPlayer = GameManager.instance.currentGameData.playerTwo;
                    break;
            }

            if (GameManager.instance.currentGameData.currentPlayer != null)
            {
                GameManager.instance.currentGameData.currentPlayer.OnItemSelected += OnItemSelectedHandler;
                GameManager.instance.currentGameData.currentPlayer.StartTurn();
            }
        }

        private IPlayer CreatePlayer(PlayerType playerType)
        {
            IPlayer player = null;

            switch (playerType)
            {
                case PlayerType.NONE:
                    player = null;
                    break;
                case PlayerType.HUMAN:
                    player = new PlayerHuman();
                    break;
                case PlayerType.NIMATRON:
                    player = new PlayerNimatron();
                    break;
            }

            return player;
        }

        private void OnItemSelectedHandler(BoardItem item)
        {
            GameManager.instance.SelectBoardItem(item);
        }
    }
}
