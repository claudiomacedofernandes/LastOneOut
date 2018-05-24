using UnityEngine;

namespace LastOneOut
{
    public class PlayerHuman : IPlayer
    {
        public event OnItemSelectedEvent OnItemSelected = null;

        public void StartTurn()
        {
            UserInput.instance.onItemSelected += OnItemSelectedHandler;
            UserInput.instance.StartInput();
        }

        public void EndTurn()
        {
            UserInput.instance.onItemSelected -= OnItemSelectedHandler;
            UserInput.instance.StopInput();
        }

        public void PauseTurn()
        {
            UserInput.instance.PauseInput();
        }

        public void ResumeTurn()
        {
            UserInput.instance.ResumeInput();
        }

        private void OnItemSelectedHandler(BoardItem item)
        {
            if (OnItemSelected != null)
                OnItemSelected(item);
        }
    }
}
