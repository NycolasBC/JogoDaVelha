using System;

namespace TicTacToe
{
    public static class GameInterface
    {
        static char[] Board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char CurrentPlayer = 'X';
        static MachinePlayer PlayerMachine = new MachinePlayer();
        static HumanPlayer HumanPlayer = new HumanPlayer();

        public static void Game()
        {
            char playerChoice = Menu.GeneralMenu();

            HumanPlayer.CurrentForm = playerChoice;
            PlayerMachine.CurrentForm = (playerChoice == 'X') ? 'O' : 'X';

            int movement;
            bool endGame = false;

            while (!endGame)
            {
                DrawBoard();
                if (CurrentPlayer == HumanPlayer.CurrentForm)
                {
                    movement = HumanPlayer.ChooseMovement(Board);

                    if (movement < 0 || movement > 9)
                    {
                        Console.WriteLine("Invalid position, try again.");
                    }
                    else
                    {
                        Board[movement - 1] = CurrentPlayer;
                        CurrentPlayer = PlayerMachine.CurrentForm;
                    }
                }
                else
                {
                    movement = PlayerMachine.ChooseMovement(Board);

                    Board[movement] = PlayerMachine.CurrentForm;
                    CurrentPlayer = HumanPlayer.CurrentForm;
                }

                endGame = CheckEndGame();
            }

            DrawBoard();
            Console.WriteLine("End of Game!");
            Console.Read();
        }

        static void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($" {Board[0]} | {Board[1]} | {Board[2]} ");
            Console.WriteLine("---|---|---");
            Console.WriteLine($" {Board[3]} | {Board[4]} | {Board[5]} ");
            Console.WriteLine("---|---|---");
            Console.WriteLine($" {Board[6]} | {Board[7]} | {Board[8]} ");
        }

        static bool CheckEndGame()
        {
            if (CheckVictory(HumanPlayer.CurrentForm))
            {
                Console.WriteLine("You Won!");
                return true;
            }
            else if (CheckVictory(PlayerMachine.CurrentForm))
            {
                Console.WriteLine("You Lost!");
                return true;
            }
            else if (CheckDraw())
            {
                Console.WriteLine("It was a draw!");
                return true;
            }
            return false;
        }

        public static bool CheckVictory(char player)
        {
            return (Board[0] == player && Board[1] == player && Board[2] == player) ||
                   (Board[3] == player && Board[4] == player && Board[5] == player) ||
                   (Board[6] == player && Board[7] == player && Board[8] == player) ||
                   (Board[0] == player && Board[3] == player && Board[6] == player) ||
                   (Board[1] == player && Board[4] == player && Board[7] == player) ||
                   (Board[2] == player && Board[5] == player && Board[8] == player) ||
                   (Board[0] == player && Board[4] == player && Board[8] == player) ||
                   (Board[2] == player && Board[4] == player && Board[6] == player);
        }

        public static bool CheckDraw()
        {
            for (int i = 0; i < Board.Length; i++)
            {
                if (Board[i] != 'X' && Board[i] != 'O')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
