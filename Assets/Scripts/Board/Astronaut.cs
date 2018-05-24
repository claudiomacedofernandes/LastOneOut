using System;
using UnityEngine;

namespace LastOneOut
{
    public class Astronaut : MonoBehaviour
    {
        [SerializeField] private Animator anim = null;

        private void OnEnable()
        {
            GameManager.instance.onGameStateChange += OnGameStateChangeHandler;
        }

        private void OnDisable()
        {
            GameManager.instance.onGameStateChange -= OnGameStateChangeHandler;
        }

        public void OnGameStateChangeHandler(object stateInfo = null)
        {
            switch (GameManager.instance.gameState)
            {
                default:
                    OnGameClosed();
                    break;
                case GameState.END:
                    OnGameEnd();
                    break;
                case GameState.RUN:
                case GameState.PAUSE:
                    break;
            }
        }

        private void OnGameEnd()
        {

            anim.SetBool("IsWaving", true);
        }

        private void OnGameClosed()
        {
            anim.SetBool("IsWaving", false);
        }
    }
}
