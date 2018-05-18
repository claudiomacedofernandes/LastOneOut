using System;
using UnityEngine;

namespace LastOneOut
{
    public class PlayerManager : MonoBehaviour
    {
        private IPlayer currentPlayer = null;
        private IPlayer playerOne = null;
        private IPlayer playerTwo = null;
        private bool getUserInput = false;

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

        private void Update()
        {
            if (getUserInput == false)
                return;

            if (Input.GetMouseButtonDown(0))
                ProcessInput(Input.mousePosition);
        }

        private void ProcessInput(Vector3 screenPosition)
        {
            if (currentPlayer == null)
                return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null && hit.collider.gameObject != null)
            {
                BoardItem item = hit.collider.gameObject.GetComponentInParent<BoardItem>();
                if (item != null)
                    currentPlayer.SelectItem(item);
            }
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

        private void OnGameTurnChangeHandler(PlayerIndex playerIndex)
        {
            switch (playerIndex)
            {
                case PlayerIndex.NONE:
                    currentPlayer = null;
                    break;
                case PlayerIndex.PLAYER_ONE:
                    currentPlayer = playerOne;
                    playerOne.StartTurn();
                    break;
                case PlayerIndex.PLAYER_TWO:
                    currentPlayer = playerTwo;
                    playerTwo.StartTurn();
                    break;
            }
        }

        private void OnItemSelectedHandler(BoardItem item)
        {
            GameManager.instance.SelectBoardItem(item);
        }

        private void OnStartUserInputHandler()
        {
            getUserInput = true;
        }

        private void OnStopUserInputHandler()
        {
            getUserInput = false;
        }

        public void Init(GameInfo gameInfo)
        {
            if (playerOne != null)
            {
                playerOne.OnItemSelected -= OnItemSelectedHandler;
                playerOne.OnStartUserInput -= OnStartUserInputHandler;
                playerOne.OnStopUserInput -= OnStopUserInputHandler;
            }
            if (playerTwo != null)
            {
                playerTwo.OnItemSelected -= OnItemSelectedHandler;
                playerTwo.OnStartUserInput -= OnStartUserInputHandler;
                playerTwo.OnStopUserInput -= OnStopUserInputHandler;
            }

            playerOne = null;
            playerTwo = null;

            CreatePlayer(out playerOne, gameInfo.playerOneType);
            CreatePlayer(out playerTwo, gameInfo.playerTwoType);

            if (playerOne != null)
            {
                playerOne.OnItemSelected += OnItemSelectedHandler;
                playerOne.OnStartUserInput += OnStartUserInputHandler;
                playerOne.OnStopUserInput += OnStopUserInputHandler;
            }
            if (playerTwo != null)
            {
                playerTwo.OnItemSelected += OnItemSelectedHandler;
                playerTwo.OnStartUserInput += OnStartUserInputHandler;
                playerTwo.OnStopUserInput += OnStopUserInputHandler;
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
    }
}
