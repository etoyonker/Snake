namespace Snake
{
    internal class Food
    {
        public void Render()
        {
            Console.CursorLeft = PosX * 2;
            Console.CursorTop = PosY + 1;

            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("O ");
            Console.ForegroundColor = oldColor;
        }

        public int PosX { get; set; }
        public int PosY { get; set; }
        public bool IsAvailable { get; set; }
    }
}
