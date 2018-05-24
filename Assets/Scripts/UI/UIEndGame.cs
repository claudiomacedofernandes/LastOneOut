using UnityEngine;
using UnityEngine.UI;

namespace LastOneOut
{
    public class UIEndGame : MonoBehaviour
    {
        [SerializeField] private Text winnerText = null;
        [SerializeField] private Text winnerTextShadow = null;

        private void OnEnable()
        {
            GameManager.instance.onGameStateChange += onGameStateChangeHandler;
        }

        private void OnDisable()
        {
            GameManager.instance.onGameStateChange -= onGameStateChangeHandler;
        }

        private void onGameStateChangeHandler(object stateInfo = null)
        {
            if (GameManager.instance.gameState == GameState.END)
                Init();
        }

        private void Init()
        {
            if (GameManager.instance.currentGameData == null)
                return;

            winnerText.text = GetWinnerName();
            winnerTextShadow.text = winnerText.text;
        }

        private string GetWinnerName()
        {
            return GameManager.instance.currentGameData.winnerPlayer == PlayerIndex.PLAYER_ONE ? "Player One" : "Player Two";
        }
    }
}
