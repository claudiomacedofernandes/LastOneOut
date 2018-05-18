using UnityEngine;

namespace LastOneOut
{
    public class PlayerNimatron : IPlayer
    {
        public event OnStartUserInputEvent OnStartUserInput = null;
        public event OnStopUserInputEvent OnStopUserInput = null;
        public event OnItemSelectedEvent OnItemSelected = null;

        public void StartTurn()
        {
            Debug.Log("Nimatron StartTurn");
        }

        public void EndTurn()
        {
            Debug.Log("Nimatron EndTurn");
        }

        public void SelectItem(BoardItem item)
        {
            if (OnItemSelected != null)
                OnItemSelected(item);
        }
    }
}
