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

        public int TimeLeft { get; private set; } 

        public int GameScore { get; private set; } = 0;

        public int GameLevel { get; private set; } = 0;

        public string ButtonText { get; set; } = "Click To Start";

        public Timer GameTimer;

        public GameManager()
        {
            Player = new PlayerModel();
            SquareBots = new List<SquareBotModel>();
            GameTimer = new Timer();
            GameTimer.Interval = 1000;
            GameTimer.Elapsed += CountDown;
            GameTimer.Enabled = true;
        }

        public async void MainLoop()
        {
            IsRunning = true;
            Player.Color = "green";
            TimeLeft = 5;
            GameLevel += 1;

            while (IsRunning)
            {
                ButtonText = "Running";
                ManageBots();
                MoveObjects();
                DetectCollisions();
                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);
            }
            if (TimeLeft == 0)
            {
                NextLevel();
            }
            else
                GameOver();
        }

        public void CountDown(object sender, ElapsedEventArgs e)
        {
            if (TimeLeft == 0)
            {   
                if (ButtonText != "Click To Start")
                    NextLevel();
            }
            else
                TimeLeft -= 1;
        }

        public void StartGame()
        {
            SquareBots.Clear();
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
            {
                Score();
                SquareBots.Remove(SquareBots.First());
            }
                
                
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

        public void Score()
        {
            GameScore += 5;
        }

        public void NextLevel()
        {
            ButtonText = "Click For Next Level";
            MainLoopCompleted?.Invoke(this, EventArgs.Empty);
            IsRunning = false;
        }

        public void GameOver()
        {
            Player.Color = "red";
            GameTimer.Dispose();
            ButtonText = "Click To Start";
            GameLevel = 0;
            GameScore = 0;
            MainLoopCompleted?.Invoke(this, EventArgs.Empty);

        }
    }

}
