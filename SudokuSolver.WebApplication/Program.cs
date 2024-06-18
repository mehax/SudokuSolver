using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SudokuSolver.BLL;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("https://www.usdoku.com", "https://usdoku.com", "https://www.livesudoku.com");
        });
});

var app = builder.Build();
app.UseCors(myAllowSpecificOrigins);

app.MapGet("/", ([FromQuery][Required] string game) =>
{
    var positions = new List<Position>();
    var board = new Board(game);
    board.OnNumberSet += (row, col, number, _) =>
    {
        positions.Add(new(row, col, number));
    };
    
    var solver = new Solver(board);
    solver.Run();

    return positions;
});

app.Run();
public record Position(int Row, int Col, int Number);