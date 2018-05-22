using UnityEngine;

namespace LastOneOut
{
    public class PlayerHuman : IPlayer
    {
        public event OnItemSelectedEvent OnItemSelected = null;

        public void StartTurn()
        {
            Debug.Log("Human StartTurn");
            InputManager.instance.onItemSelected += onItemSelectedHandler;
            InputManager.instance.StartInputTracking();
        }

        public void EndTurn()
        {
            Debug.Log("Human EndTurn");
            InputManager.instance.onItemSelected -= onItemSelectedHandler;
            InputManager.instance.StopInputTracking();
        }

        public void onItemSelectedHandler(BoardItem item)
        {
            if (OnItemSelected != null)
                OnItemSelected(item);
        }
    }
}
