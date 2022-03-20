using SudokuSolver.ConsoleApp;
using SudokuSolver.BLL;

var game = File.ReadAllText("game.txt");
IBoard board = FactoryService.CreateBoard().Init(game);
ISolver solver = FactoryService.CreateSolver().Init(board);
var display = new Display();

board.OnNumberSet += (row, col, number, algorithm) =>
{
    Console.WriteLine($"[{algorithm}][SET] {number}, on ({row + 1}, {col + 1})");
};
board.OnNumberUnmarked += (row, col, number, algorithm) =>
{
    Console.WriteLine($"[{algorithm}][UNMARK] {number}, on ({row + 1}, {col + 1})");
};

solver.Run();
display.Print(board);
