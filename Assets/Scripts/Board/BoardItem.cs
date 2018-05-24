using System;
using UnityEngine;

namespace LastOneOut
{
    public class BoardItem : MonoBehaviour
    {
        [SerializeField] private BoardItemAnimator anim = null;
        [SerializeField] private float disappearSpeed = 1.0F;
        [HideInInspector] public string id = null;
        private bool isDisappearing = false;
        private Vector3 initialPosition = Vector3.zero;
        private Vector3 endPosition = Vector3.zero;
        private float startTime = 0.0f;
        private float journeyLength = 0.0f;
        private float journeyDistance = 500.0f;

        public string Setup()
        {
            id = System.Guid.NewGuid().ToString();
            anim.onAnimationExit += OnAnimationExitHandler;
            anim.onAnimationDisappear += OnAnimationDisappearHandler;

            initialPosition = gameObject.transform.position;
            endPosition = new Vector3(initialPosition.x, initialPosition.y + journeyDistance, initialPosition.z);
            journeyLength = Vector3.Distance(initialPosition, endPosition);
            isDisappearing = false;

            return id;
        }

        private void OnAnimationExitHandler()
        {
            anim.onAnimationExit -= OnAnimationExitHandler;
            DestroyItem();
        }

        private void OnAnimationDisappearHandler()
        {
            anim.onAnimationDisappear -= OnAnimationDisappearHandler;
            startTime = Time.time;
            isDisappearing = true;
        }

        private void DestroyItem()
        {
            Destroy(gameObject);
        }

        public void Init()
        {
            SetInitState();
        }

        public void Pause()
        {
        }

        public void SetInitState()
        {
            anim.SetInitState();
        }

        public void SetVictoryState()
        {
            anim.SetVictoryState();
        }

        public void SetDefeateState()
        {
            anim.SetDefeateState();
        }

        private void Update()
        {
            if (isDisappearing == false)
                return;

            float distCovered = (Time.time - startTime) * disappearSpeed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(initialPosition, endPosition, fracJourney);
        }

    }
}
