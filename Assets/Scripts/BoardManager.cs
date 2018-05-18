using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
    public class BoardManager : MonoBehaviour
    {
        public GameObject board = null;
        public GameObject boardItemPrefab = null;
        private List<Vector3> boardPlaces = null;

        private void Start()
        {
            StoreBoardPositions();
        }

        private void OnEnable()
        {
            GameManager.instance.onGameStateChange += OnGameStateChangeHandler;
        }

        private void OnDisable()
        {
            GameManager.instance.onGameStateChange -= OnGameStateChangeHandler;
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

        public void StoreBoardPositions()
        {
            boardPlaces = new List<Vector3>();
            Transform[] places = board.GetComponentsInChildren<Transform>();
            foreach (Transform transform in places)
            {
                if (transform.gameObject == board)
                    continue;

                boardPlaces.Add(transform.position);
                Destroy(transform.gameObject);
            }
        }

        public void Init()
        {
            DestroyBoard();
            InitBoard(boardPlaces);
            GameManager.instance.SetBoardManagerReady(true);
        }

        public void DestroyBoard()
        {
            BoardItem[] items = board.GetComponentsInChildren<BoardItem>();
            foreach (BoardItem item in items)
            {
                Destroy(item.gameObject);
            }
        }

        public void InitBoard(List<Vector3> places)
        {
            foreach (Vector3 place in places)
            {
                AddItem(place);
            }
        }

        public void AddItem(Vector3 position)
        {
            GameObject newItem = Instantiate(boardItemPrefab, position, Quaternion.identity, board.transform);
            newItem.transform.localRotation = Quaternion.identity;
        }

        public void ShowBoard()
        {
            board.SetActive(true);
        }

        public void HideBoard()
        {
            board.SetActive(false);
        }
    }
}
