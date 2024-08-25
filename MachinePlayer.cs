using System;
using System.Runtime.InteropServices;

namespace TicTacToe
{
    public class MachinePlayer : IPlayers
    {
        public char CurrentForm { get; set; }

        public int ChooseMovement(char[] board)
        {
            char opponentForm = CurrentForm == 'X' ? 'O' : 'X';
            int bestValue = int.MinValue;
            int bestMovement = -1;

            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] != 'X' && board[i] != 'O')
                {
                    int movementValue = EvaluateScore(board, i, opponentForm);

                    /*  MinMax
                        board[i] = CurrentForm;
                        int movementValue = Minimax(board, 0, false, opponentForm);
                        board[i] = (char)(i + 1 + '0');
                    */

                    if (movementValue > bestValue)
                    {
                        bestValue = movementValue;
                        bestMovement = i;
                    }
                }
            }

            return bestMovement;
        }

        private int EvaluateScore(char[] board, int position, char opponentForm)
        {
            int score = 0;

            if (position == 4) score += 2;

            if (position == 0 || position == 2 || position == 6 || position == 8) score += 1;

            if (CheckOpponentLine(board, position, opponentForm) 
                || CheckOpponentColumn(board, position, opponentForm) 
                || CheckOpponentDiagonal(board, position, opponentForm))
                score -= 2;

            if (MovementPreventsVictory(board, position, opponentForm)) score += 4;
            if (MovementWinsGame(board, position)) score += 4;

            return score;
        }

        private bool CheckOpponentLine(char[] board, int position, char opponentForm)
        {
            char adversario = opponentForm;
            int linha = position / 3;

            for (int i = linha * 3; i < linha * 3 + 3; i++)
            {
                if (board[i] == adversario)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckOpponentColumn(char[] board, int position, char opponentForm)
        {
            char adversario = opponentForm;
            int coluna = position % 3; // Usa o resto da divisão por 3 para saber a coluna

            for (int i = coluna; i < 9; i += 3) // Verifica toda a coluna (0, 3, 6 para primeira coluna; 1, 4, 7 para a segunda, etc.)
            {
                if (board[i] == adversario)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckOpponentDiagonal(char[] board, int position, char opponentForm)
        {
            char adversario = opponentForm;

            // Verifica se a posição faz parte da diagonal principal (0, 4, 8)
            if (position == 0 || position == 4 || position == 8)
            {
                if (board[0] == adversario || board[4] == adversario || board[8] == adversario)
                {
                    return true;
                }
            }

            // Verifica se a posição faz parte da diagonal secundária (2, 4, 6)
            if (position == 2 || position == 4 || position == 6)
            {
                if (board[2] == adversario || board[4] == adversario || board[6] == adversario)
                {
                    return true;
                }
            }

            return false;
        }

        private bool MovementPreventsVictory(char[] board, int position, char opponentForm)
        {
            board[position] = opponentForm;
            bool preventVictory = GameInterface.CheckVictory(opponentForm);

            board[position] = (char)(position + 1 + '0');

            return preventVictory;
        }

        private bool MovementWinsGame(char[] board, int position)
        {
            board[position] = CurrentForm;
            bool winGame = GameInterface.CheckVictory(CurrentForm);

            board[position] = (char)(position + 1 + '0');

            return winGame;
        }

        private int Minimax(char[] board, int profundidade, bool isMaximizing, char opponentForm)
        {
            if (GameInterface.CheckVictory(CurrentForm)) return 10;
            if (GameInterface.CheckVictory(opponentForm)) return -10;
            if (GameInterface.CheckDraw()) return 0;

            if (isMaximizing)
            {
                int best = int.MinValue;

                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] != 'X' && board[i] != 'O')
                    {
                        board[i] = CurrentForm;
                        int value = Minimax(board, profundidade + 1, false, opponentForm);

                        board[i] = (char)(i + 1 + '0');

                        best = Math.Max(best, value);
                    }
                }

                return best;
            }
            else
            {
                int best = int.MaxValue;

                for (int i = 0; i < 9; i++)
                {
                    if (board[i] != 'X' && board[i] != 'O')
                    {
                        board[i] = opponentForm;
                        int value = Minimax(board, profundidade + 1, true, opponentForm);

                        board[i] = (char)(i + 1 + '0');

                        best = Math.Min(best, value);
                    }
                }

                return best;
            }
        }
    }
}
