# MinEffort Puzzle

Given a 3-D puzzle where the length and width of the puzzle is
a 2-D array. The height of each cell is given by the value stored
at each position in the array. Starting at (0, 0), the objective
is to move to the destination at (n - 1, m - 1) where n is the
number of rows and m is the number of columns. The goal is to
find the destination in the minimum amount of effort. The effort
of a route is defined as the maximum absolute difference between two
consecutive cells.

For example, in the following puzzle, the expected effort is 1.

[1, 3, 5]
[2, 8, 3]
[3, 4, 5]

The route with the minimum effort would be 1 -> 2 -> 3 -> 4 -> 5.
Overall, the difference between each node is only a maximum of 1.
Therefore, the final effort value is 1.
