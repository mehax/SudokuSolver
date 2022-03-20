namespace SudokuSolver.BLL.Internal;

internal class Board : IBoard
{
    private int[,] mNumbers = new int[9, 9];
    private List<int>[,] mMarked = new List<int>[9, 9];
    private bool mShouldNotify;

    public int[,] GetNumbers()
    {
        return mNumbers;
    }

    public List<int>[,] GetMarked()
    {
        return mMarked;
    }

    public int GetNumberAtPosition(int row, int col)
    {
        return mNumbers[row, col];
    }

    public List<int> GetMarkedAtPosition(int row, int col)
    {
        return mMarked[row, col];
    }

    public event IBoard.BoardUpdated? OnNumberSet;
    public event IBoard.BoardUpdated? OnNumberUnmarked;

    public IBoard Init(int[,] numbers, List<int>[,] marked, bool shouldNotify)
    {
        mNumbers = numbers;
        mMarked = marked;
        mShouldNotify = shouldNotify;
        return this;
    }

    public IBoard Init(string content)
    {
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                mMarked[i, j] = new();
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
                    SetNumber(row, col, nr, null);
                }
            }
        }

        mShouldNotify = true;
        return this;
    }

    public void ReInit(IBoard board)
    {
        mNumbers = board.GetNumbers();
        mMarked = board.GetMarked();
    }

    public void SetNumber(int row, int col, int number, string? algorithm)
    {
        mNumbers[row, col] = number;
        mMarked[row, col] = new();

        for (var i = 0; i < 9; i++)
        {
            Unmark(row, i, number, algorithm);
            Unmark(i, col, number, algorithm);
        }

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                var r = i + row / 3 * 3;
                var c = j + col / 3 * 3;

                Unmark(r, c, number, algorithm);
            }
        }

        if (mShouldNotify)
        {
            OnNumberSet?.Invoke(row, col, number, algorithm);
        }
    }

    public void Mark(int row, int col, int number)
    {
        if (!mMarked[row, col].Contains(number))
        {
            mMarked[row, col].Add(number);
        }
    }

    public bool Unmark(int row, int col, int number, string? algorithm)
    {
        if (mMarked[row, col].Contains(number))
        {
            mMarked[row, col].Remove(number);
            if (mShouldNotify)
            {
                OnNumberUnmarked?.Invoke(row, col, number, algorithm);
            }
            return true;
        }

        return false;
    }

    public IBoard Clone()
    {
        var numbers = (int[,])mNumbers.Clone();
        var marked = new List<int>[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                marked[i, j] = mMarked[i, j].ToList();
            }
        }

        return new Board().Init(numbers, marked, false);
    }

    public bool HasError()
    {
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                if (mNumbers[i, j] == 0 && !mMarked[i, j].Any())
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool IsWin()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (mNumbers[i, j] == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void StopNotifications()
    {
        mShouldNotify = false;
    }

    public void StartNotifications()
    {
        mShouldNotify = true;
    }
}