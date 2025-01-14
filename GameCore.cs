using System.Diagnostics;

namespace Snake
{
    internal class GameCore
    {
        private readonly double _updateLimiter = 180; // in ms
        private readonly Player _player;
        private readonly Food _food;
        private readonly long _initTimestamp;
        private TimeSpan _totalTime;
        private TimeSpan _lastUpdateTime;
        private ConsoleKeyInfo _currentPressedKey;

        public GameCore()
        {
            Map.Width = 20;
            Map.Height = 24;

            _player = new Player();
            _player.Segments.Add(new Point()
            {
                PosX = Map.Width / 2,
                PosY = Map.Height / 2
            });

            _food = new Food();
            _food.PlaceAtRandomPoint();

            _initTimestamp = Stopwatch.GetTimestamp();
        }

        public void Loop()
        {
            // thanks to:
            // https://gameprogrammingpatterns.com/game-loop.html
            while (true)
            {
                ProcessInput();

                _totalTime = Stopwatch.GetElapsedTime(_initTimestamp);
                if (_totalTime.TotalMilliseconds - _lastUpdateTime.TotalMilliseconds >= _updateLimiter)
                {
                    Update();
                    Render();
                    _lastUpdateTime = _totalTime;
                }
            }
        }

        private void ProcessInput()
        {
            while (Console.KeyAvailable)
            {
                var pressedKey = Console.ReadKey();

                if (_player.Segments.Count > 1)
                {
                    switch (pressedKey.Key)
                    {
                        case ConsoleKey.UpArrow when _currentPressedKey.Key != ConsoleKey.DownArrow:
                            _currentPressedKey = pressedKey;
                            break;

                        case ConsoleKey.RightArrow when _currentPressedKey.Key != ConsoleKey.LeftArrow:
                            _currentPressedKey = pressedKey;
                            break;

                        case ConsoleKey.LeftArrow when _currentPressedKey.Key != ConsoleKey.RightArrow:
                            _currentPressedKey = pressedKey;
                            break;

                        case ConsoleKey.DownArrow when _currentPressedKey.Key != ConsoleKey.UpArrow:
                            _currentPressedKey = pressedKey;
                            break;
                    }
                }
                else _currentPressedKey = pressedKey;
            }
        }

        private void Update()
        {
            _player.Move(_currentPressedKey.Key);

            if (_player.CheckSelfBite()) Environment.Exit(0);

            // if snake's coordinates of head are equal to the coordinates of food
            if (_player.Segments[0].PosX == _food.Point.PosX && _player.Segments[0].PosY == _food.Point.PosY)
            {
                _player.EatFood();
                _food.PlaceAtRandomPoint();
            }
        }

        private void Render()
        {
            Console.WriteLine($"Score: {_player.Score}, Time: {Math.Round(_totalTime.TotalSeconds, 0, MidpointRounding.ToZero)}");

            Map.Render();
            _food.Render();
            _player.Render();

            Console.CursorLeft = 0;
            Console.CursorTop = 0;
        }
    }
}
