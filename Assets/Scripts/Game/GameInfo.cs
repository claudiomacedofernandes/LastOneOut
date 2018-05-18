namespace LastOneOut
{
    public class GameInfo
    {
        public PlayerType playerOneType;
        public PlayerType playerTwoType;

        public GameInfo(PlayerType _playerOneType, PlayerType _playerTwoType)
        {
            playerOneType = _playerOneType;
            playerTwoType = _playerTwoType;
        }
    }
}
