namespace Snake
{
    internal static class Map
    {
        public static void Render()
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 1;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write("# ");
                }
                Console.WriteLine();
            }
        }

        public static int Width { get; set; }
        public static int Height { get; set; }
    }
}
