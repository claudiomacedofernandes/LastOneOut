using UnityEngine;
using UnityEngine.UI;

namespace LastOneOut
{
    public class UIPlayerTurn : MonoBehaviour
    {
        public Text textPlayer = null;

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
            textPlayer.text = GameManager.instance.currentGameData.currentPlayerIndex.ToString();
        }
    }
}
