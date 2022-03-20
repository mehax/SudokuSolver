using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SudokuSolver.BLL.Internal;
using SudokuSolver.BLL.Test.Builders;

namespace SudokuSolver.BLL.Test;

public class SolverTest
{
    private Solver mSut;

    [SetUp]
    public void Setup()
    {
        mSut = new Solver();
    }
    
    private static string[] Games =>
        new[]
        {
            "123456780000000000000000000000000000000000000000000000000000000000000000000000000",
            "123456789456000000780000000000000000000000000000000000000000000000000000000000000",
            "100000000200000000300000000400000000500000000600000000700000000800000000000000000",
            "100000000230000000450000000000000000000000000007000000000000000000000000000000000"
        };

    private static (int row, int col, List<int> marked)[] Marked =>
        new[]
        {
            (0, 8, new List<int>() { 9 }),
            (2, 2, new List<int>() { 9 }),
            (8, 0, new List<int>() { 9 }),
            (0, 1, new List<int>() { 6, 7, 8, 9 }),
            (0, 2, new List<int>() { 6, 8, 9 }),
            (1, 2, new List<int>() { 6, 8, 9 }),
            (2, 2, new List<int>() { 6, 8, 9 }),
        };
    
    [Test]
    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(2, 2)]
    public void Algo1_Run_ShouldCompleteMissingNumber(int gameIndex, int markedIndex)
    {
        var board = BuildBoard(gameIndex, markedIndex);
        var steps = 1;
        var row = Marked[markedIndex].row;
        var col = Marked[markedIndex].col;
        var number = Marked[markedIndex].marked[0];
        var algo = "Algo1";
        
        mSut.Init(board.Object).Run(steps);
        
        board.Verify(b => b.SetNumber(row, col, number, algo));
    }
    
    [Test]
    [TestCase(3, new[]{3, 4, 5, 6}, 0, 1, 7)]
    public void Algo2_Run_ShouldCompleteMissingNumber(int gameIndex, int[] markedIndex,
        int expectedRow, int expectedCol, int expectedNumber)
    {
        var board = BuildBoard(gameIndex, markedIndex);
        var steps = 1;
        var algo = "Algo2";
        
        mSut.Init(board.Object).Run(steps);
        
        board.Verify(b => b.SetNumber(expectedRow, expectedCol, expectedNumber, algo));
    }

    private Mock<IBoard> BuildBoard(int gameIndex, int[] markedIndex)
    {
        var board = new BoardBuilder()
            .WithGame(Games[gameIndex]);
        foreach (var index in markedIndex)
        {
            board.WithMarked(Marked[index].row, Marked[index].col, Marked[index].marked);
        }

        return board.Build();
    }

    private Mock<IBoard> BuildBoard(int gameIndex, int markedIndex)
    {
        return new BoardBuilder()
            .WithGame(Games[gameIndex])
            .WithMarked(Marked[markedIndex].row, Marked[markedIndex].col, Marked[markedIndex].marked)
            .Build();
    }
}