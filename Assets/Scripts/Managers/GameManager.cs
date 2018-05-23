﻿using System;
using UnityEngine;

namespace LastOneOut
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game Settings")]
        [Range(1, 10)] public int minTurnMoves = 1;
        [Range(1, 10)] public int maxTurnMoves = 3;
        [Range(0.1f, 10.0f)] public float aiWaitTime = 1.0f;
        public AIDificulty aIDifficulty = AIDificulty.HARD;

        [Header("Game State")]
        public GameState prevGameState = GameState.NONE;
        public GameState gameState = GameState.NONE;
        public GameData currentGameData = null;

        [HideInInspector] public static GameManager instance = null;
        [HideInInspector] public System.Action<object> onGameStateChange = null;
        [HideInInspector] public System.Action onGameTurnChange = null;
        [HideInInspector] public System.Action<BoardItem> onGameItemSelected = null;
        [HideInInspector] public System.Action<bool> onTurnEnabledChange = null;
        private bool boardManagerReady = false;
        private bool playerManagerReady = false;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            if (maxTurnMoves < minTurnMoves)
                maxTurnMoves = minTurnMoves;
        }

        void Start()
        {
            SetGameState(GameState.MENU);
        }

        public void Update()
        {
            if (gameState == GameState.NEW_GAME && CheckGameReadyState() == true)
                SetGameState(GameState.SETUP);
            else if (gameState == GameState.END)
                ResetGameReadyState();
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

        public void StartNewTurn(PlayerIndex playerIndex = PlayerIndex.NONE)
        {
            if (onTurnEnabledChange != null)
                onTurnEnabledChange(false);

            if (playerIndex == PlayerIndex.NONE)
                currentGameData.currentPlayerIndex = currentGameData.currentPlayerIndex == PlayerIndex.PLAYER_ONE ? PlayerIndex.PLAYER_TWO : PlayerIndex.PLAYER_ONE;
            else
                currentGameData.currentPlayerIndex = playerIndex;

            if (onGameTurnChange != null)
                onGameTurnChange();

            currentGameData.currentPlayerMoves = 0;
        }

        public void SetGameState(GameState newGameState, object stateInfo = null)
        {
            prevGameState = gameState;
            gameState = newGameState;

            if (onGameStateChange != null)
                onGameStateChange(stateInfo);
        }

        public void SetBoardManagerReady(bool newState)
        {
            boardManagerReady = newState;
        }

        public void SetPlayerManagerReady(bool newState)
        {
            playerManagerReady = newState;
        }

        public void ResetGameReadyState()
        {
            boardManagerReady = false;
            playerManagerReady = false;
        }

        public bool CheckGameReadyState()
        {
            if (gameState != GameState.NEW_GAME)
                return false;

            if (boardManagerReady == false)
                return false;

            if (playerManagerReady == false)
                return false;

            return true;
        }

        public void SelectBoardItem(BoardItem selectedItem)
        {
            if (CheckPlayerMaxMoves() == true)
                return;

            currentGameData.currentPlayerMoves++;

            if (currentGameData.currentPlayer.GetType().Equals(typeof(PlayerHuman))
                && CheckPlayerFirstMove() == true
                && onTurnEnabledChange != null)
                onTurnEnabledChange(true);

            if (onGameItemSelected != null)
                onGameItemSelected(selectedItem);

            CheckGameEnd();
        }

        public bool CheckPlayerMaxMoves()
        {
            return currentGameData.currentPlayerMoves >= maxTurnMoves;
        }

        public bool CheckPlayerFirstMove()
        {
            return currentGameData.currentPlayerMoves == minTurnMoves;
        }

        public void CheckGameEnd()
        {
            if (currentGameData.boardItems.Count == 0)
            {
                SetGameState(GameState.END);
            }
        }
    }
}