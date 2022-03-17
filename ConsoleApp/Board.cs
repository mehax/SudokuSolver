namespace ConsoleApp;

public class Board
{
    public int[,] Numbers = new int[9, 9];
    public List<int>[,] Marked = new List<int>[9, 9];

    public Board(string content)
    {
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                Marked[i, j] = new();
                for (var nr = 1; nr <= 9; nr++)
                {
                    Mark(i, j, nr);
                }
            }
        }
        
        content = content.Replace("\n", "");
        var index = 0;
        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                var nr = Convert.ToInt32(content[index] - '0');
                index++;

                if (nr != 0)
                {
                    SetNumber(row, col, nr, false);
                }
            }
        }
    }

    public void SetNumber(int row, int col, int number, bool callEvent = true)
    {
        Numbers[row, col] = number;

        for (var i = 0; i < 9; i++)
        {
            Unmark(row, i, number);
            Unmark(i, col, number);
        }

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                var r = i + row / 3 * 3;
                var c = j + col / 3 * 3;

                Unmark(r, c, number);
            }
        }

        if (callEvent)
        {
            Console.WriteLine($"Marked {number} on ({row}, {col})");
        }
    }

    public void Mark(int row, int col, int number)
    {
        if (!Marked[row, col].Contains(number))
        {
            Marked[row, col].Add(number);
        }
    }

    public bool Unmark(int row, int col, int number)
    {
        if (Marked[row, col].Contains(number))
        {
            Marked[row, col].Remove(number);
            return true;
        }

        return false;
    }

    private void ResetMarked()
    {
        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                if (Numbers[row, col] == 0)
                {
                    SetMarked(row, col, new());
                }
                else
                {
                    var marked = new List<int>();
                    for (var nr = 1; nr <= 9; nr++)
                    {
                        if (!VisitRow(row, nr) && !VisitCol(col, nr) && !VisitBlock(row, col, nr))
                        {
                            marked.Add(nr);
                        }
                    }
                    SetMarked(row, col, marked);
                }
            }
        }
    }

    private bool VisitRow(int row, int number, int? skip = null)
    {
        for (var col = 0; col < 9; col++)
        {
            if (col == skip)
            {
                continue;
            }

            if (Numbers[row, col] == number)
            {
                return true;
            }
        }

        return false;
    }

    private bool VisitCol(int col, int number, int? skip = null)
    {
        for (var row = 0; row < 9; row++)
        {
            if (row == skip)
            {
                continue;
            }

            if (Numbers[row, col] == number)
            {
                return true;
            }
        }

        return false;
    }

    private bool VisitBlock(int row, int col, int number, int? skipRow = null, int? skipCol = null)
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                var r = i + row / 3 * 3;
                var c = j + col / 3 * 3;

                if (r == skipRow && c == skipCol)
                {
                    continue;
                }
                
                if (Numbers[r, c] == number)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void SetMarked(int row, int col, List<int> numbers)
    {
        Marked[row, col] = numbers;
    }

    private int[] GetRow(int rowIndex)
    {
        var res = new int[9];
        for (int i = 0; i < 9; i++)
        {
            res[i] = Numbers[rowIndex, i];
        }

        return res;
    }

    private int[] GetCol(int colIndex)
    {
        var res = new int[9];
        for (int i = 0; i < 9; i++)
        {
            res[i] = Numbers[i, colIndex];
        }

        return res;
    }

    private int[,] GetBlock(int rowIndex, int colIndex)
    {
        var res = new int[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var row = i + rowIndex / 3 * 3;
                var col = j + colIndex / 3 * 3;

                res[i, j] = Numbers[row, col];
            }
        }

        return res;
    }
}