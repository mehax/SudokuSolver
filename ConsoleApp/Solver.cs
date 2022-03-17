namespace ConsoleApp;

public class Solver
{
    private Board mBoard;
    private List<Board> mBoardForBowman = new();
    private List<(int row, int col, int nr)> mBowmanOptions = new();
    
    public Solver(Board board)
    {
        mBoard = board.Clone();
    }

    public Board Run()
    {
        while (true)
        {
            if (Algo1())
            {
                continue;
            }

            if (Algo2())
            {
                continue;
            }

            if (Algo3())
            {
                continue;
            }

            if (Algo4())
            {
                continue;
            }

            if (Algo5())
            {
                continue;
            }

            if (BowmanBingo())
            {
                continue;
            }

            break;
        }

        return mBoard;
    }

    private bool Algo1()
    {
        var changed = false;

        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                if (mBoard.Numbers[i, j] == 0 && mBoard.Marked[i, j].Count == 1)
                {
                    mBoard.SetNumber(i, j, mBoard.Marked[i, j].First());
                    changed = true;
                }
            }
        }
        
        return changed;
    }

    private bool Algo2()
    {
        var changed = false;

        for (var number = 1; number <= 9; number++)
        {
            for (var i = 0; i < 9; i++)
            {
                var lastColIndex = -1;
                var lastRowIndex = -1;
                var countCol = 0;
                var countRow = 0;

                for (var j = 0; j < 9; j++)
                {
                    if (mBoard.Numbers[i, j] == 0 && mBoard.Marked[i, j].Contains(number))
                    {
                        countCol++;
                        lastColIndex = j;
                    }

                    if (mBoard.Numbers[j, i] == 0 && mBoard.Marked[j, i].Contains(number))
                    {
                        countRow++;
                        lastRowIndex = j;
                    }
                }

                if (countCol == 1)
                {
                    mBoard.SetNumber(i, lastColIndex, number);
                    changed = true;
                }

                if (countRow == 1)
                {
                    mBoard.SetNumber(lastRowIndex, i, number);
                    changed = true;
                }
            }

            for (var block = 0; block < 9; block++)
            {
                var lastRowIndex = -1;
                var lastColIndex = -1;
                var count = 0;

                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        var row = i + block / 3 * 3;
                        var col = j + block % 3 * 3;

                        if (mBoard.Numbers[row, col] == 0 && mBoard.Marked[row, col].Contains(number))
                        {
                            lastRowIndex = row;
                            lastColIndex = col;
                            count++;
                        }
                    }
                }

                if (count == 1)
                {
                    mBoard.SetNumber(lastRowIndex, lastColIndex, number);
                    changed = true;
                }
            }
        }
        
        return changed;
    }

    private bool Algo3()
    {
        var changed = false;

        for (var nr1 = 1; nr1 <= 9; nr1++)
        {
            for (var nr2 = nr1 + 1; nr2 <= 9; nr2++)
            {
                // rows
                for (int i = 0; i < 9; i++)
                {
                    var rowPos = new List<int>();
                    var shouldContinue = false;

                    for (int j = 0; j < 9; j++)
                    {
                        if (mBoard.Numbers[i, j] != 0)
                        {
                            continue;
                        }

                        var first = mBoard.Marked[i, j].Contains(nr1);
                        var second = mBoard.Marked[i, j].Contains(nr2);
                        
                        if (first != second)
                        {
                            shouldContinue = true;
                            break;
                        }
                        
                        if (first && second)
                        {
                            rowPos.Add(j);
                        }
                    }
                    
                    if (shouldContinue) continue;

                    if (rowPos.Count == 2)
                    {
                        for (int nr = 1; nr <= 9; nr++)
                        {
                            if (nr == nr1 || nr == nr2)
                            {
                                continue;
                            }

                            if (mBoard.Unmark(i, rowPos[0], nr)) changed = true;
                            if (mBoard.Unmark(i, rowPos[1], nr)) changed = true;
                        }
                    }
                }
                
                // cols
                for (int i = 0; i < 9; i++)
                {
                    var colPos = new List<int>();
                    var shouldContinue = false;

                    for (int j = 0; j < 9; j++)
                    {
                        if (mBoard.Numbers[j, i] != 0)
                        {
                            continue;
                        }
                        
                        var first = mBoard.Marked[j, i].Contains(nr1);
                        var second = mBoard.Marked[j, i].Contains(nr2);

                        if (first != second)
                        {
                            shouldContinue = true;
                            break;
                        }
                        
                        if (first && second)
                        {
                            colPos.Add(j);
                        }
                    }
                    
                    if (shouldContinue) continue;

                    if (colPos.Count == 2)
                    {
                        for (int nr = 1; nr <= 9; nr++)
                        {
                            if (nr == nr1 || nr == nr2)
                            {
                                continue;
                            }

                            if (mBoard.Unmark(colPos[0], i, nr)) changed = true;
                            if (mBoard.Unmark(colPos[1], i, nr)) changed = true;
                        }
                    }
                }
            }
        }

        for (var block = 0; block < 9; block++)
        {
            for (var nr1 = 1; nr1 < 9; nr1++)
            {
                for (var nr2 = nr1 + 1; nr2 <= 9; nr2++)
                {
                    var positions = new List<(int row, int col)>();
                    var shouldContinue = false;

                    for (var i = 0; i < 3; i++)
                    {
                        for (var j = 0; j < 3; j++)
                        {
                            var row = block / 3 * 3 + i;
                            var col = block % 3 * 3 + j;

                            if (mBoard.Numbers[row, col] == nr1 || mBoard.Numbers[row, col] == nr2)
                            {
                                shouldContinue = true;
                                break;
                            }

                            if (mBoard.Numbers[row, col] != 0)
                            {
                                continue;
                            }

                            var first = mBoard.Marked[row, col].Contains(nr1);
                            var second = mBoard.Marked[row, col].Contains(nr2);

                            if (first != second)
                            {
                                shouldContinue = true;
                                break;
                            }

                            if (first && second)
                            {
                                positions.Add((row, col));
                            }
                        }

                        if (shouldContinue) break;
                    }

                    if (shouldContinue)
                    {
                        continue;
                    }

                    if (positions.Count == 2)
                    {
                        for (var skip = 1; skip <= 9; skip++)
                        {
                            if (skip == nr1 || skip == nr2)
                            {
                                continue;
                            }
                            
                            positions.ForEach(pos =>
                            {
                                
                                var v = mBoard.Unmark(pos.row, pos.col, skip);
                                if (v)
                                {
                                    changed = true;
                                }
                            });
                        }
                    }
                }
            }
        }

        return changed;
    }

    private bool Algo4()
    {
        var changed = false;

        for (var nr = 1; nr <= 9; nr++)
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = i + 1; j < 9; j++)
                {
                    var row1 = new List<int>();
                    var row2 = new List<int>();
                    var col1 = new List<int>();
                    var col2 = new List<int>();

                    for (var k = 0; k < 9; k++)
                    {
                        if (mBoard.Numbers[i, k] == 0 && mBoard.Marked[i, k].Contains(nr))
                        {
                            row1.Add(k);
                        }
                        
                        if (mBoard.Numbers[j, k] == 0 && mBoard.Marked[j, k].Contains(nr))
                        {
                            row2.Add(k);
                        }
                        
                        if (mBoard.Numbers[k, i] == 0 && mBoard.Marked[k, i].Contains(nr))
                        {
                            col1.Add(k);
                        }
                        
                        if (mBoard.Numbers[k, j] == 0 && mBoard.Marked[k, j].Contains(nr))
                        {
                            col2.Add(k);
                        }
                    }

                    if (row1.Count == 2 && row2.Count == 2 && row1[0] == row2[0] && row1[1] == row2[1])
                    {
                        for (var k = 0; k < 9; k++)
                        {
                            if (k == i || k == j)
                            {
                                continue;
                            }

                            if (mBoard.Unmark(k, row1[0], nr)) changed = true;
                            if (mBoard.Unmark(k, row1[1], nr)) changed = true;
                        }
                    }

                    if (col1.Count == 2 && col2.Count == 2 && col1[0] == col2[0] && col1[1] == col2[1])
                    {
                        for (var k = 0; k < 9; k++)
                        {
                            if (k == i || k == j)
                            {
                                continue;
                            }

                            if (mBoard.Unmark(col1[0], k, nr)) changed = true;
                            if (mBoard.Unmark(col1[1], k, nr)) changed = true;
                        }
                    }
                }
            }
        }

        return changed;
    }

    private bool Algo5()
    {
        var changed = false;

        for (var nr = 1; nr <= 9; nr++)
        {
            for (var block = 0; block < 9; block++)
            {
                var positions = new List<(int row, int col)>();
                var shouldContinue = false;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var row = i + block / 3 * 3;
                        var col = j + block % 3 * 3;

                        if (mBoard.Numbers[row, col] != 0)
                        {
                            if (mBoard.Numbers[row, col] == nr)
                            {
                                shouldContinue = true;
                                break;
                            }

                            continue;
                        }

                        if (mBoard.Marked[row, col].Contains(nr))
                        {
                            positions.Add((row, col));
                        }
                    }

                    if (shouldContinue)
                    {
                        break;
                    }
                }

                if (shouldContinue || positions.Count <= 1)
                {
                    continue;
                }

                if (positions.All(p => p.row == positions.First().row))
                {
                    var row = positions.First().row;
                    var min = positions.Min(p => p.col);
                    var max = positions.Max(p => p.col);

                    for (int col = 0; col < 9; col++)
                    {
                        if (col >= min && col <= max)
                        {
                            continue;
                        }

                        if (mBoard.Unmark(row, col, nr)) changed = true;
                    }
                }

                if (positions.All(p => p.col == positions.First().col))
                {
                    var col = positions.First().col;
                    var min = positions.Min(p => p.row);
                    var max = positions.Max(p => p.row);

                    for (int row = 0; row < 9; row++)
                    {
                        if (row >= min || row <= max)
                        {
                            continue;
                        }

                        if (mBoard.Unmark(row, col, nr)) changed = true;
                    }
                }
            }
        }

        return changed;
    }

    private bool BowmanBingo()
    {
        if (mBoardForBowman.Any())
        {
            var last = mBoardForBowman.Last();
            var lastOption = mBowmanOptions.Last();
            if (mBoard.HasError())
            {
                last.Unmark(lastOption.row, lastOption.col, lastOption.nr);
                mBoard = last;
                mBoardForBowman.Remove(last);
                mBowmanOptions.Remove(lastOption);
                return true;
            }
        }

        var options = GetDoubleOptions();
        if (options == null)
        {
            return false;
        }
        
        mBoardForBowman.Add(mBoard.Clone());
        mBowmanOptions.Add(options.Value);
        mBoard.SetNumber(options.Value.row, options.Value.col, options.Value.nr);
        return true;
    }

    private (int row, int col, int nr)? GetDoubleOptions()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (mBoard.Numbers[i, j] == 0 && mBoard.Marked[i, j].Count == 2)
                {
                    return (i, j, mBoard.Marked[i, j].First());
                }
            }
        }
        
        return null;
    }
}