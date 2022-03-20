using SudokuSolver.BLL;
using SudokuSolver.BLL.Internal;

namespace SudokuSolver.ConsoleApp;

public class Display
{
    public void Print(IBoard board)
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

                var nr = board.GetNumberAtPosition(row, col) != 0 ? board.GetNumberAtPosition(row, col).ToString() : " ";
                Console.Write($" {nr} ");
            }

            Console.WriteLine("|");
        }
        Console.WriteLine("|         |         |         |");
        Console.WriteLine("|=============================|");
    }
}