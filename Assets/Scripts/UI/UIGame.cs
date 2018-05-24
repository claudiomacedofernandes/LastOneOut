using UnityEngine;

namespace LastOneOut
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] private GameObject endTurnButton = null;

        private void OnEnable()
        {
            GameManager.instance.onTurnEnabledChange += OnTurnEnabledChangeHandler;
        }

        private void OnDisable()
        {
            GameManager.instance.onTurnEnabledChange -= OnTurnEnabledChangeHandler;
        }

        private void OnTurnEnabledChangeHandler(bool enabled)
        {
            endTurnButton.SetActive(enabled);
        }
    }
}
