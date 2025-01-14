using System.Security.Cryptography;

namespace Snake
{
    internal class Food
    {
        public void PlaceAtRandomPoint()
        {
            // todo: check case when it spawns inside snake

            Point.PosX = RandomNumberGenerator.GetInt32(0, Map.Width - 1);
            Point.PosY = RandomNumberGenerator.GetInt32(0, Map.Width - 1);
        }

        public void Render()
        {
            Console.CursorLeft = Point.PosX * 2;
            Console.CursorTop = Point.PosY + 1;

            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("O ");
            Console.ForegroundColor = oldColor;
        }

        public Point Point { get; set; } = new Point() { PosX = 0, PosY = 0 };
    }
}
