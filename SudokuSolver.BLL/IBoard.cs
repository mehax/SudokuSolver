namespace SudokuSolver.BLL;

public interface IBoard
{
    public int[,] GetNumbers();
    public List<int>[,] GetMarked();
    public int GetNumberAtPosition(int row, int col);
    public List<int> GetMarkedAtPosition(int row, int col);
    delegate void BoardUpdated(int row, int col, int number, string? algorithm);
    event BoardUpdated? OnNumberSet;
    event BoardUpdated? OnNumberUnmarked;
    IBoard Init(int[,] numbers, List<int>[,] marked, bool shouldNotify);
    IBoard Init(string content);
    void ReInit(IBoard board);
    void SetNumber(int row, int col, int number, string? algorithm);
    void Mark(int row, int col, int number);
    bool Unmark(int row, int col, int number, string? algorithm);
    IBoard Clone();
    bool HasError();
    bool IsWin();
    void StopNotifications();
    void StartNotifications();
}