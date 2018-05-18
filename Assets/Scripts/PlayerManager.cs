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
                    break;
                case GameState.MENU:
                    break;
                case GameState.NEW_GAME:
                    Init(gameInfo);
                    break;
                case GameState.RUNNING:
                    break;
                case GameState.ENDED:
                    break;
                case GameState.EXIT:
                    break;
            }
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
