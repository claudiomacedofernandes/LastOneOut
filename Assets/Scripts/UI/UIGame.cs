using System;
using UnityEngine;

namespace LastOneOut
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] private GameObject endTurnButton = null;

        private void OnEnable()
        {
            GameManager.instance.onTurnEnabledChange += onTurnEnabledChangeHandler;
        }

        private void OnDisable()
        {
            GameManager.instance.onTurnEnabledChange -= onTurnEnabledChangeHandler;
        }

        private void onTurnEnabledChangeHandler(bool enabled)
        {
            endTurnButton.SetActive(enabled);
        }
    }
}
