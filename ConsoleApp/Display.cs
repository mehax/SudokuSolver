using SudokuSolver.BLL;

namespace ConsoleApp;

public class Display
{
    public void Print(Board board)
    {
        for (var row = 0; row < 9; row++)
        {
            if (row % 3 == 0)
            {
                Console.WriteLine("|=============================|");
            }
            Console.WriteLine("|         |         |         |");
            for (var col = 0; col < 9; col++)
            {
                if (col % 3 == 0)
                {
                    Console.Write("|");
                }

                var nr = board.Numbers[row, col] != 0 ? board.Numbers[row, col].ToString() : " ";
                Console.Write($" {nr} ");
            }

            Console.WriteLine("|");
        }
        Console.WriteLine("|         |         |         |");
        Console.WriteLine("|=============================|");
    }
}