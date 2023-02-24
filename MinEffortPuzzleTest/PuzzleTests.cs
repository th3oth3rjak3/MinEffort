namespace MinEffortPuzzleTest;

public class PuzzleTests
{
    [Fact]
    public void Test1()
    {
        Puzzle puzzle = new(
            new int[][]
            {
                new int[] { 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1 },
                new int[] { 1, 1, 1, 1 },
            });

        Assert.Equal(0, puzzle.SolvePuzzle());
    }

    [Fact]
    public void Test2()
    {
        Puzzle puzzle = new(
            new int[][]
            {
                new int[] { 1, 1, 1 },
                new int[] { 1, 1, 1 },
                new int[] { 1, 1, 1 },
                new int[] { 1, 1, 1 },
            }
        );

        Assert.Equal(0, puzzle.SolvePuzzle());
    }

    [Fact]
    public void Test3()
    {
        Puzzle puzzle = new(
            new int[][]
            {
                new int[] { 1, 3, 5 },
                new int[] { 2, 8, 3 },
                new int[] { 3, 4, 5 },
            }
        );
        Assert.Equal(1, puzzle.SolvePuzzle());
    }

    [Fact]
    public void Test4()
    {
        Puzzle puzzle = new(
            new int[][]
            {
                new int[] { 1, 2, 2 },
                new int[] { 3, 8, 2 },
                new int[] { 5, 3, 5 },
            }
        );
        Assert.Equal(2, puzzle.SolvePuzzle());
    }

    [Fact]
    public void Test5()
    {
        Puzzle puzzle = new(
            new int[][]
            {
                new int[] { 1, 2, 2, 3 },
                new int[] { 3, 8, 2, 5 },
                new int[] { 5, 3, 4, 8 },
            }
        );
        Assert.Equal(3, puzzle.SolvePuzzle());
    }

    [Fact]
    public void Test6()
    {
        Puzzle puzzle = new(
            new int[][]
            {
                new int[] { 1, 3, 5 },
                new int[] { 3, 8, 7 },
                new int[] { 5, 3, 9 },
                new int[] { 7, 9, 6 },
            }
        );
        Assert.Equal(3, puzzle.SolvePuzzle());
    }
}