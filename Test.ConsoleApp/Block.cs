namespace ConsoleApp;

public struct Block(int row, int col)
{
    // Just adding 8 here for now, as the board is set to 8 width
    private readonly char[] rowBlocks = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];

    public int Row { get; set; } = row;

    public int Col { get; set; } = col;

    public bool Occupied { get; set; }

    public bool HasMine { get; set; }

    public readonly int RowIndex => Row - 1;

    public readonly int ColIndex => Col - 1;

    public readonly string Position => $"{rowBlocks[RowIndex]}{Col}";
}
