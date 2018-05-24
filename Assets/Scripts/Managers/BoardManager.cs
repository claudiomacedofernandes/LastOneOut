using System;
using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
    public class BoardManager : MonoBehaviour
    {
        [Header("Board GameObjects")]
        public GameObject board = null;
        [Header("Board Prefabs")]
        public GameObject boardItemPrefab = null;

        private List<Vector3> initialBoardPlaces = null;

        private void Start()
        {
            StoreBoardPositions();
        }

        private void OnEnable()
        {
            GameManager.instance.onGameStateChange += OnGameStateChangeHandler;
            GameManager.instance.onGameItemSelected += OnGameItemSelectedHandler;
        }

        private void OnDisable()
        {
            GameManager.instance.onGameStateChange -= OnGameStateChangeHandler;
            GameManager.instance.onGameItemSelected -= OnGameItemSelectedHandler;
        }

        public void OnGameStateChangeHandler(object stateInfo = null)
        {
            switch (GameManager.instance.gameState)
            {
                case GameState.NONE:
                case GameState.MENU:
                case GameState.EXIT:
                    OnGameClosed();
                    break;
                case GameState.END:
                    OnGameEnd();
                    break;
                case GameState.NEW_GAME:
                    OnGameStart();
                    break;
                case GameState.SETUP:
                    ShowBoard();
                    break;
                case GameState.RUN:
                    OnGameRun();
                    break;
                case GameState.PAUSE:
                    OnGamePause();
                    break;
            }
        }

        public void OnGameClosed()
        {
            if (GameManager.instance.currentGameData == null)
                return;

            DestroyBoard();
            HideBoard();
        }

        public void OnGameStart()
        {
            InitBoard(initialBoardPlaces);
            GameManager.instance.SetBoardManagerReady(true);
        }

        public void OnGameEnd()
        {
        }

        public void OnGameRun()
        {
            BoardItem[] items = board.GetComponentsInChildren<BoardItem>();
            foreach (BoardItem item in items)
            {
                item.Init();
            }
        }

        public void OnGamePause()
        {
            BoardItem[] items = board.GetComponentsInChildren<BoardItem>();
            foreach (BoardItem item in items)
            {
                item.Pause();
            }
        }

        private void OnGameItemSelectedHandler(BoardItem item)
        {
            DestroyItem(item);
        }

        public void ShowBoard()
        {
            board.SetActive(true);
        }

        public void HideBoard()
        {
            board.SetActive(false);
        }

        public void StoreBoardPositions()
        {
            initialBoardPlaces = new List<Vector3>();
            Transform[] places = board.GetComponentsInChildren<Transform>();
            foreach (Transform transform in places)
            {
                if (transform.gameObject == board)
                    continue;

                initialBoardPlaces.Add(transform.position);
                Destroy(transform.gameObject);
            }
        }

        public void InitBoard(List<Vector3> places)
        {
            foreach (Vector3 place in places)
            {
                AddItem(place);
            }
        }

        public BoardItem CreateItem(Vector3 position)
        {
            GameObject item = Instantiate(boardItemPrefab, position, Quaternion.identity, board.transform);
            item.transform.localRotation = Quaternion.identity;
            return item.GetComponent<BoardItem>();
        }

        public void AddItem(Vector3 position)
        {
            BoardItem item = CreateItem(position);
            string itemId = item.Setup();
            if (GameManager.instance.currentGameData.boardItems != null)
                GameManager.instance.currentGameData.boardItems.Add(itemId, item);
        }

        public void DestroyItem(BoardItem item)
        {
            if (GameManager.instance.currentGameData.boardItems != null)
                GameManager.instance.currentGameData.boardItems.Remove(item.id);

            if(GameManager.instance.currentGameData.boardItems.Count > 0)
                item.SetVictoryState();
            else
                item.SetDefeateState();
        }

        public void DestroyBoard()
        {
            if(GameManager.instance.currentGameData.boardItems != null)
                GameManager.instance.currentGameData.boardItems.Clear();
            BoardItem[] items = board.GetComponentsInChildren<BoardItem>();
            foreach (BoardItem item in items)
            {
                Destroy(item.gameObject);
            }
        }
    }
}
