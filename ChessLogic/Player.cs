namespace ChessLogic
{
    public enum Player
    {
        none,
        white,
        black
    }
    public static class PlayerExtensions
    {
        public static Player Opponent(this Player player)
        {
            return player switch
            {
                Player.white => Player.black,
                Player.black => Player.white,
                _ => Player.none,
            };
        }
    }
}
