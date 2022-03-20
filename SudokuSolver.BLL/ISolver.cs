namespace SudokuSolver.BLL;

public interface ISolver
{
    ISolver Init(IBoard board);
    void Run(int? steps = null);
}