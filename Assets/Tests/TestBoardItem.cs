using UnityEngine;

namespace LastOneOut
{
    public class TestBoardItem : MonoBehaviour
    {
        [SerializeField] private BoardItem item = null;

        void Start()
        {
            item.Setup();
            item.SetInitState();
        }

        void OnEnable()
        {
            UserInput.instance.onItemSelected += OnItemSelectedHandler;
            UserInput.instance.StartInput();
        }

        void OnDisable()
        {
            UserInput.instance.onItemSelected -= OnItemSelectedHandler;
            UserInput.instance.StopInput();
        }

        void OnItemSelectedHandler(BoardItem _item)
        {
            item.SetDefeateState();
        }

    }
}
