namespace ConsoleApp;

public class Game(Board board, int size, int lives, IConsoleService consoleService)
{
    private readonly int _size = size;
    private readonly int _lives = lives;
    private readonly Board _board = board;
    private readonly IConsoleService _consoleService = consoleService;

    public void Run()
    {
        _board.Initialise(_size);
        int livesLeft = _lives;
        int totalMoves = 0;
        _consoleService.WriteLine($"Start! You have {livesLeft} lives.");

        while (livesLeft > 0)
        {
            _consoleService.WriteLine("Enter your move (1 - up, 2 - down, 3 - right, 4 - left) : ");
            var input = _consoleService.ReadLine();

            var move = MoveConverter.ParseInput(input);
            if (move is Move.Unknown)
            {
                _consoleService.WriteLine("Invalid input! Please try again.");
                continue;
            }

            var xyMove = MoveConverter.GetMoveDimensions(move);

            if (_board.IsOutOfBounds(xyMove))
            {
                _consoleService.WriteLine("Out of bounds! Please try again.");
                continue;
            }

            totalMoves++;
            _board.SetNewPosition(xyMove);

            if (_board.CurrentPosition.HasMine)
            {
                _consoleService.WriteLine($"You hit a mine! You have {--livesLeft} lives left.");

                if (livesLeft is 0)
                {
                    _consoleService.WriteLine($"Game over! Final position - {_board.CurrentPosition.Position}, Total moves - {totalMoves}");
                    return;
                }

            }

            if (_board.CheckWin())
            {
                _consoleService.WriteLine("You win!");
                return;
            }

            _consoleService.WriteLine($"Current position - {_board.CurrentPosition.Position}, Total moves - {totalMoves}");
        }
    }
}