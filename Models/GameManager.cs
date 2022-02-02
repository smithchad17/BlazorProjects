using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game.Models
{
    public class GameManager
    {

        public PlayerModel Player { get; private set; }

        public TimerModel Timer { get; private set; }

        public bool IsRunning { get; private set; } = false;

        public event EventHandler MainLoopCompleted;

        public GameManager()
        {
            Player = new PlayerModel();
            Timer = new TimerModel();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            Player.Color = "green";

            while (IsRunning)
            {
                MoveObjects();
                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);
            }
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                Player = new PlayerModel();
                MainLoop();
            }
        }

        public void MovePlayer(string direction)
        {
            if (IsRunning)
            {
                if (direction == "up")
                    Player.MoveUp();
                if (direction == "down")
                    Player.MoveDown();
                if (direction == "left")
                    Player.MoveLeft();
                if (direction == "right")
                    Player.MoveRight();
            }

        }

        public void MoveObjects()
        {
            
        }

        public void GameOver()
        {
            IsRunning = false;
            Player.Color = "red";
        }
    }

}
