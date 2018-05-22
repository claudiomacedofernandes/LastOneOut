using System.Collections.Generic;
using UnityEngine;

namespace LastOneOut
{
    public class GameData
    {
        public IPlayer playerOne = null;
        public IPlayer playerTwo = null;
        public IPlayer currentPlayer = null;
        public PlayerIndex currentPlayerIndex = PlayerIndex.NONE;
        public Dictionary<string, BoardItem> boardItems = null;

        public GameData()
        {
            boardItems = new Dictionary<string, BoardItem>();
        }
    }
}
