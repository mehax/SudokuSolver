using ConsoleApp;
using SudokuSolver.BLL;

var game = File.ReadAllText("game.txt");
var board = new Board(game);
var solver = new Solver(board);
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
