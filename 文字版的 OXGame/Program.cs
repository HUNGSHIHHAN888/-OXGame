using System.Reflection.Emit;
using static 文字版的_OXGame.Program;

namespace 文字版的_OXGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextOXGame game = new TextOXGame();
            game.StartGame();
        }
        public class OXGameEngine
        {
            private char[,] gameMarkers;

            public OXGameEngine()
            {
                gameMarkers = new char[3, 3];
                ResetGame();
            }

            public void SetMarker(int x, int y, char player)
            {
                if (IsValidMove(x, y))
                {
                    gameMarkers[x, y] = player;
                }
                else
                {
                    throw new ArgumentException("Invalid move!");
                }
            }

            public void ResetGame()
            {
                gameMarkers = new char[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        gameMarkers[i, j] = ' ';
                    }
                }
            }

            public char IsWinner()
            {
                // 檢查橫向
                for (int i = 0; i < 3; i++)
                {
                    if (gameMarkers[i, 0] != ' ' && gameMarkers[i, 0] == gameMarkers[i, 1] && gameMarkers[i, 1] == gameMarkers[i, 2])
                    {
                        return gameMarkers[i, 0];
                    }
                }

                // 檢查縱向
                for (int j = 0; j < 3; j++)
                {
                    if (gameMarkers[0, j] != ' ' && gameMarkers[0, j] == gameMarkers[1, j] && gameMarkers[1, j] == gameMarkers[2, j])
                    {
                        return gameMarkers[0, j];
                    }
                }

                // 檢查對角線
                if (gameMarkers[0, 0] != ' ' && gameMarkers[0, 0] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 2])
                {
                    return gameMarkers[0, 0];
                }

                if (gameMarkers[0, 2] != ' ' && gameMarkers[0, 2] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 0])
                {
                    return gameMarkers[0, 2];
                }

                return ' '; // 沒有贏家出現
            }

            private bool IsValidMove(int x, int y)
            {
                if (x < 0 || x >= 3 || y < 0 || y >= 3)
                {
                    return false;
                }

                if (gameMarkers[x, y] != ' ')
                {
                    return false;
                }

                return true;
            }

            public char GetMarker(int x, int y)
            {
                return gameMarkers[x, y];
            }

            // 其他遊戲相關的方法...
        }
    }
    public class TextOXGame
    {
        private OXGameEngine engine;
        private char currentPlayer;

        public TextOXGame()
        {
            engine = new OXGameEngine();
            currentPlayer = 'X'; // 開始時由玩家X先
        }

        public void StartGame()
        {
            Console.WriteLine("Welcome to Text-based OX Game!");

            do
            {
                Console.Clear();
                DisplayBoard();
                Console.WriteLine($"Player {currentPlayer}'s turn.");
                Console.Write("Enter row number and column number (0-2): ");
                string[] data = Console.ReadLine().Split(" ");
                int x = int.Parse(data[0]);
                int y = int.Parse(data[1]);
                try
                {
                    engine.SetMarker(x, y, currentPlayer);
                    Console.Clear();
                    DisplayBoard();
                    char winner = engine.IsWinner();
                    if (winner != ' ')
                    {
                        Console.WriteLine($"Winner: {winner}");
                        return;
                    }
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X'; // 換另一位玩家
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }

            } while (!IsGameOver());

            Console.WriteLine("It's a draw!");
        }

        private bool IsGameOver()
        {
            return engine.IsWinner() != ' ' || IsBoardFull();
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (engine.GetMarker(i, j) == ' ')
                    {
                        return false; // 找到一個空位，棋盤還沒滿
                    }
                }
            }
            return true; // 所有位置都有棋子，棋盤滿了
        }

        private void DisplayBoard()
        {
            Console.WriteLine("Game Board:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(engine.GetMarker(i, j));
                    if (j < 2)
                    {
                        Console.Write(" | ");
                    }
                }
                Console.WriteLine();
                if (i < 2)
                {
                    Console.WriteLine("---------");
                }
            }
            Console.WriteLine();
        }
    }
}