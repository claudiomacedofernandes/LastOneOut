using System;
using UnityEngine;

namespace LastOneOut
{
    public class PlayerManager : MonoBehaviour
    {

        private IPlayer playerOne = null;
        private IPlayer playerTwo = null;

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
                    Init((GameInfo) stateInfo);
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

        private void OnGameTurnChangeHandler(PlayerIndex playerIndex)
        {

        }

        public void Init(GameInfo gameInfo)
        {
            CreatePlayer(playerOne, gameInfo.playerOneType);
            CreatePlayer(playerTwo, gameInfo.playerTwoType);

            GameManager.instance.SetPlayerManagerReady(true);
        }

        private void CreatePlayer(IPlayer player, PlayerType playerType)
        {
            Debug.Log("CreatePlayer: " + playerType.ToString());
        }
    }
}
