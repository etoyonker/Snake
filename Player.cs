namespace Snake
{
    internal class Player
    {
        public List<Point> Segments { get; set; } = [];
        public int Score { get; set; }

        public void Move(ConsoleKey currentPressedKey)
        {
            // move body
            for (int i = Segments.Count - 1; i > 0; i--)
            {
                Segments[i].PosX = Segments[i - 1].PosX;
                Segments[i].PosY = Segments[i - 1].PosY;
            }

            // move head
            switch (currentPressedKey)
            {
                case ConsoleKey.UpArrow:
                    Segments[0].PosY--;
                    break;

                case ConsoleKey.RightArrow:
                    Segments[0].PosX++;
                    break;

                case ConsoleKey.LeftArrow:
                    Segments[0].PosX--;
                    break;

                case ConsoleKey.DownArrow:
                    Segments[0].PosY++;
                    break;
            }

            // check if player is out of bounds horizontally
            if (Segments[0].PosX < 0)
                Segments[0].PosX = Map.Width - 1;
            else if (Segments[0].PosX >= Map.Width)
                Segments[0].PosX = 0;

            // check if player is out of bounds vertically
            if (Segments[0].PosY < 0)
                Segments[0].PosY = Map.Height - 1;
            else if (Segments[0].PosY >= Map.Height)
                Segments[0].PosY = 0;
        }

        public void EatFood()
        {
            Score++;

            Segments.Add(new Point()
            {
                PosX = Segments[^1].PosX,
                PosY = Segments[^1].PosY
            });
        }

        public bool CheckSelfBite()
        {
            return Segments.Count > 1 && Segments[1..].Exists(e => e.PosX == Segments[0].PosX && e.PosY == Segments[0].PosY);
        }

        public void Render()
        {
            for (int i = 0; i < Segments.Count; i++)
            {
                Console.CursorLeft = Segments[i].PosX * 2;
                Console.CursorTop = Segments[i].PosY + 1;

                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("O ");
                Console.ForegroundColor = oldColor;
            }
        }
    }
}
