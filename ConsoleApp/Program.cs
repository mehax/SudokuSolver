using ConsoleApp;

var game = File.ReadAllText("game.txt");
var board = new Board(game);
var solver = new Solver(board);
var display = new Display();

var solved = solver.Run();
display.Print(solved);
