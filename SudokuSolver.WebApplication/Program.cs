using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SudokuSolver.BLL;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("https://www.usdoku.com", "https://usdoku.com", "https://www.livesudoku.com");
        });
});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/", ([FromQuery][Required] string game) =>
{
    var positions = new List<Position>();
    IBoard board = FactoryService.CreateBoard().Init(game);
    board.OnNumberSet += (row, col, number, _) =>
    {
        positions.Add(new(row, col, number));
    };
    
    ISolver solver = FactoryService.CreateSolver().Init(board);
    solver.Run();

    return positions;
});

app.Run();
public record Position(int Row, int Col, int Number);