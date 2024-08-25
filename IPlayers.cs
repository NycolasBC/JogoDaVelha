namespace TicTacToe
{
    public interface IPlayers
    {
        char CurrentForm { get; set; }

        int ChooseMovement(char[] board);
    }
}
