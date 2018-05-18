using UnityEngine;

namespace LastOneOut
{
    public delegate void OnStartUserInputEvent();
    public delegate void OnStopUserInputEvent();
    public delegate void OnItemSelectedEvent(BoardItem item);

    public interface IPlayer
    {
        event OnStartUserInputEvent OnStartUserInput;
        event OnStopUserInputEvent OnStopUserInput;
        event OnItemSelectedEvent OnItemSelected;

        void StartTurn();
        void EndTurn();
        void SelectItem(BoardItem item);
    }
}