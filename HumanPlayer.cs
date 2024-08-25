using System;

namespace TicTacToe
{
    public class HumanPlayer : IPlayers
    {
        public char CurrentForm { get; set; }

        public int ChooseMovement(char[] board)
        {
            Console.WriteLine("\nChoose the position (1-9):");
            int movement = int.Parse(Console.ReadLine());

            if (board[movement - 1] != 'X' && board[movement - 1] != 'O')
            {
                return movement;
            }
            else
            {
                return -1;
            }
        }
    }
}
