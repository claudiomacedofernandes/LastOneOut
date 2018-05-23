using UnityEngine;

namespace LastOneOut
{
    public class PlayerNimatron : IPlayer
    {
        public event OnItemSelectedEvent OnItemSelected = null;

        public void StartTurn()
        {
            AIInput.instance.onItemSelected += OnItemSelectedHandler;
            AIInput.instance.StartInput();
        }

        public void EndTurn()
        {
            AIInput.instance.onItemSelected -= OnItemSelectedHandler;
            AIInput.instance.StopInput();
        }

        private void OnItemSelectedHandler(BoardItem item)
        {
            if (OnItemSelected != null)
                OnItemSelected(item);
        }

    }
}
