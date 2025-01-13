namespace Snake
{
    internal class Player
    {
        public void Move(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (Score == 0)
                        PosY--;
                    break;

                case ConsoleKey.RightArrow:
                    _player.PosX++;
                    break;

                case ConsoleKey.LeftArrow:
                    _player.PosX--;
                    break;

                case ConsoleKey.DownArrow:
                    _player.PosY++;
                    break;
            }

            // Проверяем, не вышел ли игрок за границы массива по горизонтали
            if (_player.PosX < 0)
                _player.PosX = Map.Width - 1;
            else if (_player.PosX >= Map.Width)
                _player.PosX = 0;

            // Проверяем, не вышел ли игрок за границы массива по вертикали
            if (_player.PosY < 0)
                _player.PosY = Map.Height - 1;
            else if (_player.PosY >= Map.Height)
                _player.PosY = 0;
        }

        public void Render()
        {
            for (int i = 0; i < Score + 1; i++)
            {
                Console.CursorLeft = PosXList[i] * 2;
                Console.CursorTop = PosYList[i] + 1;

                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("X ");
                Console.ForegroundColor = oldColor;
            }
        }

        public List<int> PosXList { get; set; } = new List<int>();
        public List<int> PosYList { get; set; } = new List<int>();
        public int Score { get; set; }
    }
}
