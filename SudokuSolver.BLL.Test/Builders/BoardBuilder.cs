using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace SudokuSolver.BLL.Test.Builders;

public class BoardBuilder
{
    private Mock<IBoard> mBoard = new();

    public BoardBuilder()
    {
        mBoard.Setup(m => m.GetNumberAtPosition(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
        mBoard.Setup(m => m.GetMarkedAtPosition(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<int>());
    }

    public BoardBuilder WithGame(string s)
    {
        for (var index = 0; index < s.Length; index++)
        {
            var row = index / 9;
            var col = index % 9;
            var number = s[index] - '0';
            mBoard.Setup(m => m.GetNumberAtPosition(row, col)).Returns(number);
        }

        return this;
    }

    public BoardBuilder WithNumber(int row, int col, int number)
    {
        mBoard.Setup(m => m.GetNumberAtPosition(row, col)).Returns(number);
        return this;
    }

    public BoardBuilder WithMarked(int row, int col, List<int> marked)
    {
        mBoard.Setup(m => m.GetMarkedAtPosition(row, col)).Returns(marked);
        return this;
    }

    public Mock<IBoard> Build()
    {
        return mBoard;
    }
}