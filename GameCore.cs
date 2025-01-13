using System.Diagnostics;
using System.Security.Cryptography;

namespace Snake
{
    internal class GameCore
    {
        private Player _player;
        private Food _food;
        private ConsoleKeyInfo _lastPressedKey;

        private long _initTimestamp;
        private TimeSpan _totalTime;
        private TimeSpan _lastUpdateTime;
        private double _updateLimiter = 180; // in ms

        public GameCore()
        {
            Map.Width = 20;
            Map.Height = 24;

            _player = new Player();
            _player.PosXList.Add(Map.Width / 2);
            _player.PosYList.Add(Map.Height / 2);

            _food = new Food()
            {
                PosX = RandomNumberGenerator.GetInt32(0, Map.Width - 1),
                PosY = RandomNumberGenerator.GetInt32(0, Map.Width - 1),
            };

            _initTimestamp = Stopwatch.GetTimestamp();
        }

        public void Loop()
        {
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
                _lastPressedKey = Console.ReadKey();
        }

        private void Update()
        {
            

            // Проверяем, не съел ли змий еду
            if (_player.PosX == _food.PosX && _player.PosY == _food.PosY)
            {
                _player.Score++;
                _food.PosX = RandomNumberGenerator.GetInt32(0, Map.Width - 1);
                _food.PosY = RandomNumberGenerator.GetInt32(0, Map.Width - 1);
            }
        }

        private void Render()
        {
            Console.WriteLine($"Score: {_player.Score}, Time: {_totalTime.Seconds}");
            
            Map.Render();
            _food.Render();
            _player.Render();

            Console.CursorLeft = 0;
            Console.CursorTop = 0;
        }
    }
}
