
using ConsoleApp;

var game = File.ReadAllText("game.txt");
var board = new Board(game);
var solver = new Solver(board);
var display = new Display(board);

solver.Run(() =>
{
    Console.Clear();
    display.Print();
    // Console.ReadLine();
    return 0;
});
display.Print();

Console.WriteLine("");
