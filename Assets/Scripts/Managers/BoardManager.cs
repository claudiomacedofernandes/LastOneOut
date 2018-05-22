using System;
using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
    public class BoardManager : MonoBehaviour
    {
        public GameObject board = null;
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

        public void OnGameStateChangeHandler(GameState newGameState, object stateInfo = null)
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
                case GameState.SETUP:
                    ShowBoard();
                    break;
                case GameState.RUN:
                    break;
                case GameState.END:
                    break;
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

        public void Init()
        {
            DestroyBoard();
            InitBoard(initialBoardPlaces);
            GameManager.instance.SetBoardManagerReady(true);
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
            string itemId = item.Init();
            if (GameManager.instance.currentGameData.boardItems != null)
                GameManager.instance.currentGameData.boardItems.Add(itemId, item);
        }

        public void DestroyItem(BoardItem item)
        {
            if (GameManager.instance.currentGameData.boardItems != null)
                GameManager.instance.currentGameData.boardItems.Remove(item.id);
            Destroy(item.gameObject);
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
