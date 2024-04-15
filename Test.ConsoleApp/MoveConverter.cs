namespace ConsoleApp;
public static class MoveConverter
{
    public static Move ParseInput(string input)
    {
        _ = Enum.TryParse<Move>(input, out var move);

        return move;
    }

    public static (int row, int col) GetMoveDimensions(Move inputMove)
    {
        return inputMove switch
        {
            Move.Up => (-1, 0),
            Move.Down => (1, 0),
            Move.Right => (0, 1),
            Move.Left => (0, -1),
            _ => (0, 0)
        };
    }
}
