namespace UnitTests
{
    using ConsoleApp;

    using FluentAssertions;

    using Moq;

    public class Tests
    {
        private readonly List<(int, int)> _mines =
            [
                (1, 1),
                (2, 2),
                (3, 3),
                (4, 4),
                (5, 5),
                (6, 6)
            ];
        private readonly Mock<IConsoleService> _consoleServiceMock = new();
        private readonly Mock<IMinesBuilder> _minesBuilderMock = new();
        private readonly Board _board;
        private Game _game = default!;

        public Tests()
        {
            _board = new Board(_minesBuilderMock.Object);
        }

        [Fact]
        public void WhenGameStarts_InitialPositionShouldBeA1()
        {
            // Arrange
            _game = new Game(_board, 8, 0, _consoleServiceMock.Object);
            _minesBuilderMock.Setup(m => m.GetMines(It.IsAny<int>()))
                .Returns(_mines[..1]);

            // Act
            _game.Run();

            //// Assert
            _board.CurrentPosition.Position
                .Should().Be("A1");
        }

        [Fact]
        public void WhenMoveIsOutOfBounds_ShouldNotifyUser()
        {
            // Arrange
            _game = new Game(_board, 8, 1, _consoleServiceMock.Object);
            _consoleServiceMock.SetupSequence(m => m.ReadLine())
                .Returns("1")
                .Returns("3")
                .Returns("2");
            _minesBuilderMock.Setup(m => m.GetMines(It.IsAny<int>()))
                .Returns(_mines[..1]);

            // Act
            _game.Run();

            //// Assert
            _consoleServiceMock.Verify(m =>
                m.WriteLine("Out of bounds! Please try again."), Times.Once);
        }

        [Fact]
        public void WhenMineIsHit_ShouldReduceTheLivesRemaining()
        {
            // Arrange
            _game = new Game(_board, 8, 1, _consoleServiceMock.Object);
            _consoleServiceMock.SetupSequence(m => m.ReadLine())
                .Returns("3")
                .Returns("2");
            _minesBuilderMock.Setup(m => m.GetMines(It.IsAny<int>()))
                .Returns(_mines[..1]);

            // Act
            _game.Run();

            //// Assert
            _consoleServiceMock.Verify(m =>
                m.WriteLine("Game over! Final position - B2, Total moves - 2"), Times.Once);
        }

        [Fact]
        public void WhenMineIsHit_AndNoLivesLeft_ShouldFinishTheGame()
        {
            // Arrange
            _game = new Game(_board, 8, 1, _consoleServiceMock.Object);
            _consoleServiceMock.SetupSequence(m => m.ReadLine())
                .Returns("3")
                .Returns("2");
            _minesBuilderMock.Setup(m => m.GetMines(It.IsAny<int>()))
                .Returns(_mines[..1]);

            // Act
            _game.Run();

            //// Assert
            _consoleServiceMock.Verify(m =>
                m.WriteLine("Start! You have 1 lives."), Times.Once);
            _consoleServiceMock.Verify(m =>
                m.WriteLine("You hit a mine! You have 0 lives left."), Times.Once);
            _consoleServiceMock.Verify(m =>
                m.WriteLine("Game over! Final position - B2, Total moves - 2"), Times.Once);

        }

        [Fact]
        public void WhenTheBoardIsOccupied_AndNoMineIsHit_ShouldFinishTheGame()
        {
            // Arrange
            _game = new Game(_board, 3, 1, _consoleServiceMock.Object);
            _consoleServiceMock.SetupSequence(m => m.ReadLine())
                .Returns("3")
                .Returns("3")
                .Returns("2")
                .Returns("2")
                .Returns("4")
                .Returns("4")
                .Returns("1");
            _minesBuilderMock.Setup(m => m.GetMines(It.IsAny<int>()))
                .Returns(_mines[..1]);

            // Act
            _game.Run();

            //// Assert
            _consoleServiceMock.Verify(m =>
                m.WriteLine("Start! You have 1 lives."), Times.Once);
            _consoleServiceMock.Verify(m =>
                m.WriteLine("You hit a mine! You have 0 lives left."), Times.Never);
            _consoleServiceMock.Verify(m =>
                m.WriteLine("You win!"), Times.Once);
        }
    }
}