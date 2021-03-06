﻿namespace LastOneOut
{
    public delegate void OnItemSelectedEvent(BoardItem item);

    public interface IPlayer
    {
        event OnItemSelectedEvent OnItemSelected;

        void StartTurn();
        void EndTurn();
        void PauseTurn();
        void ResumeTurn();
    }
}