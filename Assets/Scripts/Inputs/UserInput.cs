using UnityEngine;

namespace LastOneOut
{
    public class UserInput : MonoBehaviour
    {

        [HideInInspector] public static UserInput instance = null;

        public System.Action<BoardItem> onItemSelected = null;
        private bool lastTrackInput = false;
        private bool trackInput = false;
        private float lastClick = 0f;
        private readonly float waitInterval = 0.1f;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        private void Update()
        {
            if (trackInput == false)
                return;

            lastClick -= Time.deltaTime;
            if (lastClick > 0)
                return;

            if (Input.GetMouseButtonDown(0))
                ProcessInput(Input.mousePosition);
        }

        private void ProcessInput(Vector3 screenPosition)
        {
            if (onItemSelected == null)
                return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null && hit.collider.gameObject != null)
            {
                BoardItem item = hit.collider.gameObject.GetComponentInParent<BoardItem>();
                if (item != null)
                    onItemSelected(item);

                lastClick = waitInterval;
            }
        }

        public void StartInput()
        {
            lastClick = waitInterval;
            trackInput = true;
        }

        public void StopInput()
        {
            lastClick = waitInterval;
            trackInput = false;
        }

        public void PauseInput()
        {
            lastTrackInput = trackInput;
            StopInput();
        }

        public void ResumeInput()
        {
            if (lastTrackInput == false)
                return;

            lastTrackInput = false;
            StartInput();
        }
    }
}
