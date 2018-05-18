using UnityEngine;

namespace LastOneOut
{
    public class PlayerHuman : IPlayer
    {
        public event OnStartUserInputEvent OnStartUserInput = null;
        public event OnStopUserInputEvent OnStopUserInput = null;
        public event OnItemSelectedEvent OnItemSelected = null;

        public void StartTurn()
        {
            Debug.Log("Human StartTurn");
            if (OnStartUserInput != null)
                OnStartUserInput();
        }

        public void EndTurn()
        {
            Debug.Log("Human EndTurn");
            if (OnStopUserInput != null)
                OnStopUserInput();
        }

        public void SelectItem(BoardItem item)
        {
            if (OnItemSelected != null)
                OnItemSelected(item);
        }
    }
}
