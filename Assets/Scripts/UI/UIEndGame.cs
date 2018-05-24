using UnityEngine;
using UnityEngine.UI;

namespace LastOneOut
{
    public class UIEndGame : MonoBehaviour
    {
        [SerializeField] private Text winnerText = null;
        [SerializeField] private Text winnerTextShadow = null;

        void OnEnable()
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
