using System;
using UnityEngine;

namespace LastOneOut
{
    public class Drone : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1.0F;
        private Vector3 parkedPosition = Vector3.zero;
        private Vector3 flyingPosition = Vector3.zero;
        private Vector3 initialPosition = Vector3.zero;
        private Vector3 endPosition = Vector3.zero;
        private bool isMoving = false;
        private float startTime = 0.0f;
        private float journeyLength = 0.0f;
        private float journeyDistance = 0.3f;

        private void OnEnable()
        {
            if(parkedPosition == Vector3.zero)
            {
                flyingPosition = transform.localPosition;
                parkedPosition = new Vector3(flyingPosition.x, flyingPosition.y - journeyDistance, flyingPosition.z);
                transform.localPosition = parkedPosition;
            }

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
                    Disappear();
                    break;
                case GameState.SETUP:
                    Appear();
                    break;
                case GameState.LOADING:
                case GameState.RUN:
                case GameState.PAUSE:
                    break;
            }
        }

        private void Appear()
        {
            if (transform.localPosition != flyingPosition)
            {
                MoveTo(flyingPosition);
            }
        }

        private void Disappear()
        {
            if (transform.localPosition != parkedPosition)
            {
                MoveTo(parkedPosition);
            }
        }

        private void MoveTo(Vector3 destination)
        {
            if (destination == Vector3.zero)
                return;

            initialPosition = transform.localPosition;
            endPosition = destination;
            journeyLength = Vector3.Distance(initialPosition, endPosition);
            startTime = Time.time;
            isMoving = true;
        }

        private void Update()
        {
            if (isMoving == false)
                return;

            float distCovered = (Time.time - startTime) * moveSpeed;
            float fracJourney = distCovered / journeyLength;
            transform.localPosition = Vector3.Lerp(initialPosition, endPosition, fracJourney);
        }
    }
}
