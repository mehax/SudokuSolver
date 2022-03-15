namespace ConsoleApp;

public class Display
{
    private readonly Board mBoard;

    public Display(Board board)
    {
        mBoard = board;
    }

    public void Print()
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

                var nr = mBoard.Numbers[row, col] != 0 ? mBoard.Numbers[row, col].ToString() : " ";
                Console.Write($" {nr} ");
            }

            Console.WriteLine("|");
        }
        Console.WriteLine("|         |         |         |");
        Console.WriteLine("|=============================|");
    }
}