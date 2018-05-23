using UnityEngine;

namespace LastOneOut
{
    public class BoardItem : MonoBehaviour
    {
        [HideInInspector] public string id = null;

        public string Init()
        {
            id = System.Guid.NewGuid().ToString();
            return id;
        }

    }
}