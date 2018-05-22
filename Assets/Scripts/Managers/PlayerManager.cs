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

        public void OnGameStateChangeHandler(GameState newGameState, object stateInfo = null)
        {
            switch (newGameState)
            {
                case GameState.NONE:
                    break;
                case GameState.MENU:
                    break;
                case GameState.NEW_GAME:
                    Init((GameInfo)stateInfo);
                    break;
                case GameState.SETUP:
                    break;
                case GameState.RUN:
                    break;
                case GameState.END:
                    break;
                case GameState.EXIT:
                    break;
            }
        }

        private void OnGameTurnChangeHandler()
        {
            switch (GameManager.instance.currentGameData.currentPlayerIndex)
            {
                case PlayerIndex.NONE:
                    GameManager.instance.currentGameData.currentPlayer = null;
                    break;
                case PlayerIndex.PLAYER_ONE:
                    GameManager.instance.currentGameData.currentPlayer = GameManager.instance.currentGameData.playerOne;
                    GameManager.instance.currentGameData.playerOne.StartTurn();
                    break;
                case PlayerIndex.PLAYER_TWO:
                    GameManager.instance.currentGameData.currentPlayer = GameManager.instance.currentGameData.playerTwo;
                    GameManager.instance.currentGameData.playerTwo.StartTurn();
                    break;
            }
        }

        public void Init(GameInfo gameInfo)
        {
            if (GameManager.instance.currentGameData.playerOne != null)
            {
                GameManager.instance.currentGameData.playerOne.OnItemSelected -= OnItemSelectedHandler;
            }
            if (GameManager.instance.currentGameData.playerTwo != null)
            {
                GameManager.instance.currentGameData.playerTwo.OnItemSelected -= OnItemSelectedHandler;
            }

            GameManager.instance.currentGameData.playerOne = null;
            GameManager.instance.currentGameData.playerTwo = null;

            CreatePlayer(out GameManager.instance.currentGameData.playerOne, gameInfo.playerOneType);
            CreatePlayer(out GameManager.instance.currentGameData.playerTwo, gameInfo.playerTwoType);

            if (GameManager.instance.currentGameData.playerOne != null)
            {
                GameManager.instance.currentGameData.playerOne.OnItemSelected += OnItemSelectedHandler;
            }
            if (GameManager.instance.currentGameData.playerTwo != null)
            {
                GameManager.instance.currentGameData.playerTwo.OnItemSelected += OnItemSelectedHandler;
            }

            GameManager.instance.SetPlayerManagerReady(true);
        }

        private void CreatePlayer(out IPlayer player, PlayerType playerType)
        {
            player = null;

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
        }

        private void OnItemSelectedHandler(BoardItem item)
        {
            GameManager.instance.SelectBoardItem(item);
        }
    }
}
