using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace game.Models
{
    public class GameManager
    {

        public PlayerModel Player { get; private set; }

        public List<SquareBotModel> SquareBots { get; private set; }

        public bool IsRunning { get; private set; } = false;

        public event EventHandler MainLoopCompleted;

        public int TimeLeft { get; private set; } = 30;

        public Timer GameTimer;

        public GameManager()
        {
            Player = new PlayerModel();
            SquareBots = new List<SquareBotModel>();
            GameTimer = new Timer();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            Player.Color = "green";
            TimeLeft = 30;
            GameTimer.Interval = 1000;
            GameTimer.Elapsed += CountDown;
            GameTimer.Enabled = true;

            while (IsRunning)
            {
                ManageBots();
                MoveObjects();
                DetectCollisions();
                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);
            }
            GameOver();
        }

        public void CountDown(object sender, ElapsedEventArgs e)
        {
            if (TimeLeft == 0)
                IsRunning = false;
            else
                TimeLeft -= 1;
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                Player = new PlayerModel();
                GameTimer = new Timer();
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
            foreach(var bot in SquareBots)
            {
                bot.Move();
            }
            
        }

        public void ManageBots()
        {
            if (!SquareBots.Any())
            {
                SquareBots.Add(new SquareBotModel());
            }

            if (SquareBots.First().IsOffScreen())
                SquareBots.Remove(SquareBots.First());
                
        }

        public void DetectCollisions()
        {
            if (Player.HitWall())
                IsRunning = false;

            List<int> xPlayer = Enumerable.Range(Player.DistanceFromLeft, 25).ToList();
            List<int> yPlayer = Enumerable.Range(Player.DistanceFromGround, 25).ToList();

            foreach (var bot in SquareBots)
            {
                List<int> xBot = Enumerable.Range(bot.FromLeft, 35).ToList();
                List<int> yBot = Enumerable.Range(bot.FromGround, 35).ToList();

                if (xPlayer.Intersect(xBot).Count() > 0 && yPlayer.Intersect(yBot).Count() > 0)
                {
                    IsRunning = false;
                }
            }
        }

        public void GameOver()
        {
            Player.Color = "red";
            GameTimer.Dispose();
            SquareBots.Clear();
            MainLoopCompleted?.Invoke(this, EventArgs.Empty);

        }
    }

}
