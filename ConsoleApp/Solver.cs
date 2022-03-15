namespace ConsoleApp;

public class Solver
{
    private readonly Board mBoard;
    
    public Solver(Board board)
    {
        mBoard = board;
    }

    public void Run(Func<int> callback)
    {
        while (true)
        {
            callback();
            
            if (Algo1())
            {
                continue;
            }

            if (Algo2())
            {
                continue;
            }

            break;
        }
    }

    public bool Algo1()
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

    public bool Algo2()
    {
        return false;
    }
}