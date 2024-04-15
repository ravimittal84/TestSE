namespace ConsoleApp;

public class Board(IMinesBuilder minesBuilder)
{
    private int _size = 8;
    private readonly IMinesBuilder _minesBuilder = minesBuilder;

    public Block[,] Grid { get; private set; } = default!;
    public Block CurrentPosition { get; private set; } = default!;

    public virtual void Initialise(int size)
    {
        _size = size;

        Grid = new Block[size, size];

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Grid[row, col] = new Block(row + 1, col + 1);
            }
        }

        SetMines(size);
        CurrentPosition = Grid[0, 0];
        Grid[0, 0].Occupied = true;
    }

    public void SetNewPosition((int row, int col) move)
    {
        (var newRowIndex, var newColIndex) = GetNewPosition(move);
        Grid[newRowIndex, newColIndex].Occupied = true;
        CurrentPosition = Grid[newRowIndex, newColIndex];
    }

    private void SetMines(int size)
    {
        var minesToAdd = _minesBuilder.GetMines(size);
        foreach ((int row, int col) in minesToAdd)
        {
            Grid[row, col].HasMine = true;
        }
    }

    // Not efficient, would probably revisit if I had time.
    public bool CheckWin() =>
        Grid.Cast<Block>().All(b => b.Occupied || b.HasMine);

    internal bool IsOutOfBounds((int, int) move)
    {
        (var newRowIndex, var newColIndex) = GetNewPosition(move);
        return newRowIndex < 0 || newColIndex < 0 || newRowIndex >= _size || newColIndex >= _size;
    }

    private (int, int) GetNewPosition((int row, int col) move)
    {
        var newRowIndex = CurrentPosition.RowIndex + move.row;
        var newColIndex = CurrentPosition.ColIndex + move.col;

        return (newRowIndex, newColIndex);
    }
}