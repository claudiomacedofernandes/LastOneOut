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

        private void OnGameTurnChangeHandler(PlayerIndex playerIndex)
        {
            textPlayer.text = playerIndex.ToString();
        }
    }
}
