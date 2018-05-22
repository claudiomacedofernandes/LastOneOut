using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
    public class InputManager : MonoBehaviour
    {

        [HideInInspector] public static InputManager instance = null;

        public System.Action<BoardItem> onItemSelected = null;
        private bool trackInput = false;

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
            }
        }

        public void StartInputTracking()
        {
            trackInput = true;
        }

        public void StopInputTracking()
        {
            trackInput = false;
        }
    }
}
