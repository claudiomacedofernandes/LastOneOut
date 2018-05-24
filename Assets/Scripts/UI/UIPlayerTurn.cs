using UnityEngine;
using UnityEngine.UI;

namespace LastOneOut
{
    public class UIPlayerTurn : MonoBehaviour
    {
        [SerializeField] private Text playerText = null;
        [SerializeField] private Text playerShadowText = null;

        private void OnEnable()
        {
            GameManager.instance.onGameTurnChange += OnGameTurnChangeHandler;
        }

        private void OnDisable()
        {
            GameManager.instance.onGameTurnChange -= OnGameTurnChangeHandler;
        }

        private void OnGameTurnChangeHandler()
        {
            playerText.text = GetCurrentPlayerName();
            playerShadowText.text = playerText.text;
        }

        private string GetCurrentPlayerName()
        {
            return GameManager.instance.currentGameData.currentPlayerIndex == PlayerIndex.PLAYER_TWO ? "Player Two" : "Player One";
        }
    }
}
