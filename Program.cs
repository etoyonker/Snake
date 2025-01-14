namespace Snake
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;

            var gameCore = new GameCore();

            gameCore.Loop();

            Console.CursorVisible = true;
        }
    }
}
