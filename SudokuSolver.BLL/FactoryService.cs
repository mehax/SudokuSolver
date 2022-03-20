using SudokuSolver.BLL.Internal;

namespace SudokuSolver.BLL;

public static class FactoryService
{
    public static IBoard CreateBoard()
    {
        return new Board();
    }

    public static ISolver CreateSolver()
    {
        return new Solver();
    }
}