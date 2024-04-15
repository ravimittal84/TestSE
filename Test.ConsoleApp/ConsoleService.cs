namespace ConsoleApp
{
    public interface IConsoleService
    {
        string ReadLine();
        void WriteLine(string message);
    }

    public class ConsoleService : IConsoleService
    {
        public void WriteLine(string message) =>
            Console.WriteLine(message);

        public string ReadLine() =>
            Console.ReadLine() ?? string.Empty;
    }
}
