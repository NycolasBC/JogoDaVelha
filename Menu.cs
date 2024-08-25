using System;

namespace TicTacToe
{
    public static class Menu
    { 
        public static char GeneralMenu()
        {
            Console.WriteLine("Wlcome to Tic tac toe!");
            Console.WriteLine("Choose 'X' or 'O':");

            char choice = ' ';
            while (choice != 'X' && choice != 'O')
            {
                choice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
            }

            return choice;
        }
    }
}
