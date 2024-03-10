namespace SudokuSolver.BLL;

public class Solver
{
    private Board mBoard;
    private List<Board> mBoardForBowman = new();
    private List<(int row, int col, int nr)> mBowmanOptions = new();

    public Solver(Board board)
    {
        mBoard = board;
    }

    public Board Run()
    {
        while (true)
        {
            if (SingleCandidate())
            {
                continue;
            }

            if (SinglePosition())
            {
                continue;
            }

            if (LockedPairs())
            {
                continue;
            }

            if (PointingTriples())
            {
                continue;
            }

            if (BoxLineReduction())
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

    /// <summary>
    /// This appears to be the "Single Candidate" or "Naked Singles" technique. It finds cells where only one number is possible (i.e., only one candidate number is left for that cell) and fills in those cells with the respective number.
    /// </summary>
    /// <returns></returns>
    private bool SingleCandidate()
    {
        var algorithm = nameof(SingleCandidate);
        var changed = false;

        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                if (mBoard.Numbers[i, j] == 0 && mBoard.Marked[i, j].Count == 1)
                {
                    mBoard.SetNumber(i, j, mBoard.Marked[i, j].First(), algorithm);
                    changed = true;
                }
            }
        }

        return changed;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    private bool SinglePosition()
    {
        var algorithm = nameof(SinglePosition);
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
                    mBoard.SetNumber(i, lastColIndex, number, algorithm);
                    changed = true;
                }

                if (countRow == 1)
                {
                    mBoard.SetNumber(lastRowIndex, i, number, algorithm);
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
                    mBoard.SetNumber(lastRowIndex, lastColIndex, number, algorithm);
                    changed = true;
                }
            }
        }

        return changed;
    }

    /// <summary>
    /// This might be the "Pointing Pairs" or "Pointing Triples" technique. It involves identifying when candidates in a block point to a single row or column, allowing for the elimination of those candidates from other cells in that row or column outside of the block.
    /// </summary>
    /// <returns></returns>
    private bool LockedPairs()
    {
        var algorithm = nameof(LockedPairs);
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

                            if (mBoard.Unmark(i, rowPos[0], nr, algorithm)) changed = true;
                            if (mBoard.Unmark(i, rowPos[1], nr, algorithm)) changed = true;
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

                            if (mBoard.Unmark(colPos[0], i, nr, algorithm)) changed = true;
                            if (mBoard.Unmark(colPos[1], i, nr, algorithm)) changed = true;
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
                                var v = mBoard.Unmark(pos.row, pos.col, skip, algorithm);
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

    /// <summary>
    /// This seems to be a form of "Naked Pairs" or "Locked Pairs". When two cells in a row, column, or block can only contain the same two numbers, those two numbers can be removed from the candidates list of all other cells in the same row, column, or block.
    /// </summary>
    /// <returns></returns>
    private bool PointingTriples()
    {
        var algorithm = nameof(PointingTriples);
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

                            if (mBoard.Unmark(k, row1[0], nr, algorithm)) changed = true;
                            if (mBoard.Unmark(k, row1[1], nr, algorithm)) changed = true;
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

                            if (mBoard.Unmark(col1[0], k, nr, algorithm)) changed = true;
                            if (mBoard.Unmark(col1[1], k, nr, algorithm)) changed = true;
                        }
                    }
                }
            }
        }

        return changed;
    }

    /// <summary>
    /// This could be related to the "Box Line Reduction" or "Line-Box" technique. If a number can only appear in a row or column within a specific block, you can remove that number from the candidates in the same row or column in other blocks.
    /// </summary>
    /// <returns></returns>
    private bool BoxLineReduction()
    {
        var algorithm = nameof(BoxLineReduction);
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

                        if (mBoard.Unmark(row, col, nr, algorithm)) changed = true;
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

                        if (mBoard.Unmark(row, col, nr, algorithm)) changed = true;
                    }
                }
            }
        }

        return changed;
    }

    /// <summary>
    /// This is a brute force technique, also known as "Bowman's Bingo," used as a last resort. It involves making a guess between two possible numbers for a cell, then continuing to solve the puzzle until a contradiction is found, indicating the guess was incorrect, or the puzzle is solved, confirming the guess was correct.
    /// </summary>
    /// <returns></returns>
    private bool BowmanBingo()
    {
        var algorithm = nameof(BowmanBingo);
        if (mBoardForBowman.Any())
        {
            var last = mBoardForBowman.Last();
            var lastOption = mBowmanOptions.Last();
            if (mBoard.HasError())
            {
                mBoardForBowman.Remove(last);
                mBowmanOptions.Remove(lastOption);
                mBoard.ReInit(last);
                if (!mBoardForBowman.Any())
                {
                    mBoard.StartNotifications();
                }

                mBoard.Unmark(lastOption.row, lastOption.col, lastOption.nr, algorithm);
                return true;
            }

            if (mBoard.IsWin())
            {
                mBoardForBowman.Remove(last);
                mBowmanOptions.Remove(lastOption);
                mBoard.ReInit(last);
                if (!mBoardForBowman.Any())
                {
                    mBoard.StartNotifications();
                }

                mBoard.SetNumber(lastOption.row, lastOption.col, lastOption.nr, algorithm);
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
        mBoard.StopNotifications();
        mBoard.SetNumber(options.Value.row, options.Value.col, options.Value.nr, algorithm);
        return true;
    }

    /// <summary>
    /// This technique looks for rows (or columns) where a number appears as a candidate in exactly two cells and these cells line up exactly in two rows (or columns). If an X-Wing is found, the number can be removed from the candidates of all other cells in the columns (or rows).
    /// </summary>
    /// <returns></returns>
    private bool XWing()
    {
        var algorithm = nameof(XWing);
        var changed = false;

        // Check rows for X-Wing pattern
        for (var num = 1; num <= 9; num++)
        {
            for (var row1 = 0; row1 < 8; row1++)
            {
                for (var row2 = row1 + 1; row2 < 9; row2++)
                {
                    var colsInRow1 = FindCandidateColumnsForRow(num, row1);
                    var colsInRow2 = FindCandidateColumnsForRow(num, row2);

                    if (colsInRow1.Count != 2 || !colsInRow1.SequenceEqual(colsInRow2)) continue;
                    
                    // Found X-Wing pattern, remove num from other cells in these columns
                    for (var row = 0; row < 9; row++)
                    {
                        if (row == row1 || row == row2) continue;
                        foreach (var col in colsInRow1)
                        {
                            if (mBoard.Unmark(row, col, num, algorithm)) changed = true;
                        }
                    }
                }
            }
        }

        // Similar logic for columns, swapping row and column loops

        return changed;
    }
    
    /// <summary>
    /// An extension of the X-Wing technique, Swordfish involves three rows (or columns) and three columns (or rows) where a number appears as a candidate in exactly two or three cells in each of the three rows (or columns), and these cells form a rectangle or an irregular shape. This configuration allows the number to be removed from other cells in the involved columns (or rows).
    /// </summary>
    /// <returns></returns>
    bool Swordfish()
    {
        var algorithm = nameof(Swordfish);
        var changed = false;

        // Check rows for Swordfish pattern
        for (var num = 1; num <= 9; num++)
        {
            for (var row1 = 0; row1 < 7; row1++)
            {
                for (var row2 = row1 + 1; row2 < 8; row2++)
                {
                    for (var row3 = row2 + 1; row3 < 9; row3++)
                    {
                        var cols = new HashSet<int>(FindCandidateColumnsForRow(num, row1));
                        cols.UnionWith(FindCandidateColumnsForRow(num, row2));
                        cols.UnionWith(FindCandidateColumnsForRow(num, row3));

                        if (cols.Count != 3) continue;
                        
                        // Found Swordfish pattern, remove num from other cells in these columns
                        for (var row = 0; row < 9; row++)
                        {
                            if (row == row1 || row == row2 || row == row3) continue;

                            foreach (var col in cols)
                            {
                                if (mBoard.Unmark(row, col, num, algorithm)) changed = true;
                            }
                        }
                    }
                }
            }
        }

        // Similar logic for columns, swapping row and column loops

        return changed;
    }


    private List<int> FindCandidateColumnsForRow(int number, int row)
    {
        var candidateColumns = new List<int>();

        // Assume mBoard.Marked[row, col] holds the set of candidate numbers for each cell
        for (var col = 0; col < 9; col++)
        {
            // Check if 'number' is a candidate in the cell at (row, col)
            if (mBoard.Marked[row, col].Contains(number))
            {
                candidateColumns.Add(col);
            }
        }

        return candidateColumns;
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