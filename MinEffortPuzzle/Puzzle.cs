namespace MinEffortPuzzle;


using HeapQueue;
using HeapQueue.Models;

public class Puzzle
{
    public Puzzle(int[][] board)
    {
        Board = board;
        End = new(board.Length - 1, board[0].Length - 1);
        VisitedNodes = CreateVisitedNodes(board);
    }

    /// <summary>
    /// A 2D array of integers representing the board.
    /// </summary>
    private int[][] Board { get; set; }

    /// <summary>
    /// A min heap priority queue of coordinates to visit.
    /// </summary>
    private MinHeap Queue { get; set; } = new();

    /// <summary>
    /// The starting coordinates for the puzzle.
    /// </summary>
    private Coordinates Start { get; set; } = new(0, 0);

    /// <summary>
    /// The current coordinates for the puzzle.
    /// </summary>
    private Coordinates Current { get; set; } = new(0, 0);

    /// <summary>
    /// The ending coordinates for the puzzle.
    /// </summary>
    private Coordinates End { get; set; }

    /// <summary>
    /// A 2D array of booleans representing the visited nodes.
    /// </summary>
    private bool[][] VisitedNodes { get; set; }

    /// <summary>
    /// The amount of effort required to solve the puzzle.
    /// </summary>
    private int MinEffort { get; set; } = 0;

    public int SolvePuzzle()
    {
        Queue.Enqueue(new(0, Start));
        while (Current != End)
        {
            ExploreAdjacentNodes();
        }
        return MinEffort;
    }

    /// <summary>
    /// Explores the adjacent nodes of the current node and adds them to the queue.
    /// </summary>
    private void ExploreAdjacentNodes()
    {
        var activeNode = Queue.Dequeue();
        Current = activeNode.Coordinates;
        if (Current == End)
        {
            MinEffort = activeNode.Value;
            return;
        }
        if (WasVisited(Current)) return;
        ExploreLeft(activeNode);
        ExploreRight(activeNode);
        ExploreUp(activeNode);
        ExploreDown(activeNode);
        VisitCurrent();
    }

    /// <summary>
    /// Explores the node to the left of the current node and adds it to the queue.
    /// </summary>
    /// <param name="activeNode">The current node used as the basis for exploration.</param>
    private void ExploreLeft(Node activeNode) =>
        Explore(activeNode, CanMoveLeft, LeftCoordinates, EffortLeft);

    /// <summary>
    /// Explores the node to the right of the current node and adds it to the queue.
    /// </summary>
    /// <param name="activeNode">The current node used as the basis for exploration.</param>
    private void ExploreRight(Node activeNode) =>
        Explore(activeNode, CanMoveRight, RightCoordinates, EffortRight);

    /// <summary>
    /// Explores the node above the current node and adds it to the queue.
    /// </summary>
    /// <param name="activeNode">The current node used as the basis for exploration.</param>
    private void ExploreUp(Node activeNode) =>
        Explore(activeNode, CanMoveUp, UpCoordinates, EffortUp);

    /// <summary>
    /// Explores the node below the current node and adds it to the queue.
    /// </summary>
    /// <param name="activeNode">The current node used as the basis for exploration.</param>
    private void ExploreDown(Node activeNode) =>
        Explore(activeNode, CanMoveDown, DownCoordinates, EffortDown);


    /// <summary>
    /// Explores the node specified by the provided parameters and adds it to the queue.
    /// </summary>
    /// <param name="activeNode">The current node used as the basis for exploration.</param>
    /// <param name="canMove">A function that returns true if the node can be visited, false otherwise.</param>
    /// <param name="coordinates">A function that returns the coordinates of the node to explore.</param>
    /// <param name="effort">A function that returns the effort required to move to the node to explore.</param>
    private void Explore(Node activeNode, Func<bool> canMove, Func<Coordinates> coordinates, Func<int> effort)
    {
        if (canMove() && !WasVisited(coordinates()))
        {
            int maxEffort = int.Max(effort(), activeNode.Value);
            Node node = new(maxEffort, coordinates());
            Queue.Enqueue(node);
        }
    }

    /// <summary>
    /// Checks to see if the node to the left of the current node can be visited.
    /// </summary>
    /// <returns>True if the node can be visited, false otherwise.</returns>
    private bool CanMoveLeft() => CurrentColumn() > 0;

    /// <summary>
    /// Checks to see if the node to the right of the current node can be visited.
    /// </summary>
    /// <returns>True if the node can be visited, false otherwise.</returns>
    private bool CanMoveRight() => CurrentColumn() < NumberOfColumns() - 1;

    /// <summary>
    /// Checks to see if the node above the current node can be visited.
    /// </summary>
    /// <returns>True if the node can be visited, false otherwise.</returns>
    private bool CanMoveUp() => CurrentRow() > 0;

    /// <summary>
    /// Checks to see if the node below the current node can be visited.
    /// </summary>
    /// <returns>True if the node can be visited, false otherwise.</returns>
    private bool CanMoveDown() => CurrentRow() < NumberOfRows() - 1;

    /// <summary>
    /// Calculates the effort required to move to the node to the left of the current node.
    /// </summary>
    /// <returns>The effort required to move to the node to the left of the current node.</returns>
    private int EffortLeft() => Effort(LeftCoordinates());

    /// <summary>
    /// Calculates the effort required to move to the node to the right of the current node.
    /// </summary>
    /// <returns>The effort required to move to the node to the right of the current node.</returns>
    private int EffortRight() => Effort(RightCoordinates());

    /// <summary>
    /// Calculates the effort required to move to the node above the current node.
    /// </summary>
    /// <returns>The effort required to move to the node above the current node.</returns>
    private int EffortUp() => Effort(UpCoordinates());

    /// <summary>
    /// Calculates the effort required to move to the node below the current node.
    /// </summary>
    /// <returns>The effort required to move to the node below the current node.</returns>
    private int EffortDown() => Effort(DownCoordinates());

    /// <summary>
    /// Calculates the effort required to move to the specified node.
    /// </summary>
    /// <param name="coordinates">The coordinates of the node to calculate the effort for.</param>
    /// <returns>The effort required to move to the specified node.</returns>
    private int Effort(Coordinates coordinates) => 
        int.Abs(Board[coordinates.Row][coordinates.Column] - Board[Current.Row][Current.Column]);

    /// <summary>
    /// Gets the coordinates of the node to the left of the current node.
    /// </summary>
    /// <returns>The coordinates of the node to the left of the current node.</returns>
    private Coordinates LeftCoordinates() => new(Current.Row, Current.Column - 1);

    /// <summary>
    /// Gets the coordinates of the node to the right of the current node.
    /// </summary>
    /// <returns>The coordinates of the node to the right of the current node.</returns>
    private Coordinates RightCoordinates() => new(Current.Row, Current.Column + 1);

    /// <summary>
    /// Gets the coordinates of the node above the current node.
    /// </summary>
    /// <returns>The coordinates of the node above the current node.</returns>
    private Coordinates UpCoordinates() => new(Current.Row - 1, Current.Column);

    /// <summary>
    /// Gets the coordinates of the node below the current node.
    /// </summary>
    /// <returns>The coordinates of the node below the current node.</returns>
    private Coordinates DownCoordinates() => new(Current.Row + 1, Current.Column);

    /// <summary>
    /// Gets the row of the current node and is zero based.
    /// </summary>
    /// <returns>The row of the current node.</returns>
    private int CurrentRow() => Current.Row;

    /// <summary>
    /// Gets the column of the current node and is zero based.
    /// </summary>
    /// <returns>The column of the current node.</returns>
    private int CurrentColumn() => Current.Column;

    /// <summary>
    /// Gets the number of rows in the board.
    /// </summary>
    /// <returns>The number of rows in the board.</returns>
    private int NumberOfRows() => Board.Length;

    /// <summary>
    /// Gets the number of columns in the board.
    /// </summary>
    /// <returns>The number of columns in the board.</returns>
    private int NumberOfColumns() => Board[0].Length;

    /// <summary>
    /// Checks if the current node has already been visited.
    /// </summary>
    /// <param name="coords">The coordinates of the node to check.</param>
    /// <returns>True if the node has been visited, false otherwise.</returns>
    private bool WasVisited(Coordinates coords) => VisitedNodes[coords.Row][coords.Column];

    /// <summary>
    /// Marks the current node as visited.
    /// </summary>
    private void VisitCurrent() => VisitedNodes[Current.Row][Current.Column] = true;

    /// <summary>
    /// Creates a data structure to keep track of visited nodes.
    /// </summary>
    /// <param name="board">The board to create the data structure for.</param>
    /// <returns>A 2D array of booleans.</returns>
    private static bool[][] CreateVisitedNodes(int[][] board)
    {
        bool[][] visitedNodes = new bool[board.Length][];
        for (int i = 0; i < board.Length; i++)
        {
            visitedNodes[i] = new bool[board[i].Length];
            for (int j = 0; j < board[i].Length; j++)
                visitedNodes[i][j] = false;
        }

        return visitedNodes;
    }
}
