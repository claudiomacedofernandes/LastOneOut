using System.Collections;
using System.Linq;
using UnityEngine;

namespace LastOneOut
{
    public class AIInput : MonoBehaviour
    {
        [HideInInspector] public static AIInput instance = null;

        public System.Action<BoardItem> onItemSelected = null;
        private bool lastComputeInput = false;
        private bool computeInput = false;
        private int numMoves = 0;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        private int DecideMoves()
        {
            int multiplier = 0;

            switch (GameManager.instance.aIDifficulty)
            {
                case AIDificulty.EASY:
                    multiplier = Random.Range(2, GameManager.instance.maxTurnMoves);
                    break;
                case AIDificulty.NORMAL:
                    multiplier = Random.Range(1, GameManager.instance.maxTurnMoves + 1);
                    break;
                case AIDificulty.HARD:
                    multiplier = GameManager.instance.maxTurnMoves;
                    break;
            }

            int division = Mathf.FloorToInt((GameManager.instance.currentGameData.boardItems.Count - 1) / multiplier);
            int optimal = (division * multiplier) + 1;
            int tmp = GameManager.instance.currentGameData.boardItems.Count - optimal;
            return tmp == 0 ? GameManager.instance.maxTurnMoves : tmp;
        }

        private BoardItem DecideItem()
        {
            int index = UnityEngine.Random.Range(0, GameManager.instance.currentGameData.boardItems.Count);
            string selectedKey = GameManager.instance.currentGameData.boardItems.ElementAt(index).Key;
            return GameManager.instance.currentGameData.boardItems[selectedKey];
        }

        private IEnumerator DoMoves()
        {
            yield return new WaitForSeconds(GameManager.instance.aiWaitTime);

            while (numMoves > 0)
            {
                if (computeInput == true)
                {
                    BoardItem item = DecideItem();
                    if (onItemSelected != null)
                        onItemSelected(item);

                    numMoves--;
                }

                yield return new WaitForSeconds(GameManager.instance.aiWaitTime);
            }

            GameManager.instance.StartNewTurn();

            yield return null;
        }

        public void StartInput()
        {
            computeInput = true;
            numMoves = DecideMoves();
            StopCoroutine("DoMoves");
            StartCoroutine("DoMoves");
        }

        public void StopInput()
        {
            computeInput = false;
            StopCoroutine("DoMoves");
            numMoves = 0;
        }

        public void PauseInput()
        {
            lastComputeInput = computeInput;
            StopInput();
        }

        public void ResumeInput()
        {
            if (lastComputeInput == false)
                return;

            lastComputeInput = false;
            StartInput();
        }
    }
}
