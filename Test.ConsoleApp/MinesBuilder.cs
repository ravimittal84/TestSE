
namespace ConsoleApp
{
    public interface IMinesBuilder
    {
        List<(int, int)> GetMines(int size);
    }

    public class MinesBuilder : IMinesBuilder
    {
        public List<(int, int)> GetMines(int size)
        {
            var minesToAdd = size * size / 10;
            var mines = new List<(int, int)>(minesToAdd);
            var rand = new Random();
            do
            {
                // Starting from index of 1,1 because 0,0 (A1) is the starting position as per our assumptions
                // and we don't want a mine on that block
                int randomRow = rand.Next(1, size);
                int randomCol = rand.Next(1, size);
                if (!mines.Contains((randomRow, randomCol)))
                {
                    mines.Add((randomRow, randomCol));
                    minesToAdd--;
                }
            } while (minesToAdd > 0);

            return mines;
        }
    }
}
