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

        public int GameSpeed { get; set; }

        public double GameTimeInterval { get; set; } = 750;

        public string ButtonText { get; set; } = "Click To Start";

        public Timer GameTimer;

        public Timer BotTimer;

        public GameManager()
        {
            Player = new PlayerModel();
            SquareBots = new List<SquareBotModel>();
            GameTimer = new Timer();
            GameTimer.Interval = 1000;
            GameTimer.Elapsed += CountDown;
            GameTimer.Enabled = true;
            BotTimer = new Timer();
            BotTimer.Interval = GameTimeInterval;
            BotTimer.Elapsed += ManageBots;
            BotTimer.Enabled = true;
        }

        public async void MainLoop()
        {
            IsRunning = true;
            Player.Color = "green";
            TimeLeft = 10;
            GameLevel += 1;

            while (IsRunning)
            {
               ButtonText = "Running";
               if (!SquareBots.Any())
                {
                    GameSpeed = 5;
                }
                else
                {
                    MoveObjects();
                    DetectCollisions();
                }
                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);
            }
            //NextLevel();
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
                BotTimer = new Timer();
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
                bot.Move(GameSpeed);
            }
            
        }

        public void ManageBots(object sender, ElapsedEventArgs e)
        {
            if (GameLevel > 1)
            {
                if (SquareBots.Count() < GameLevel + 1)
                    SquareBots.Add(new SquareBotModel());
            }
            else 
            {
                if (!SquareBots.Any())
                    SquareBots.Add(new SquareBotModel());
            } 
                
        }

        public void DetectCollisions()
        {
            if (Player.HitWall())
                GameOver();

            if (SquareBots.First().IsOffScreen())
            {
                Score();
                SquareBots.Remove(SquareBots.First());
            }

            List<int> xPlayer = Enumerable.Range(Player.DistanceFromLeft, 25).ToList();
            List<int> yPlayer = Enumerable.Range(Player.DistanceFromGround, 25).ToList();

            foreach (var bot in SquareBots)
            {
                List<int> xBot = Enumerable.Range(bot.FromLeft, 35).ToList();
                List<int> yBot = Enumerable.Range(bot.FromGround, 35).ToList();

                if (xPlayer.Intersect(xBot).Count() > 0 && yPlayer.Intersect(yBot).Count() > 0)
                {
                    GameOver();
                }
                
            }
        }

        public void Score()
        {
            GameScore += 5;
        }

        public void NextLevel()
        {
            GameTimeInterval = GameTimeInterval / 2;

            if (GameLevel > 1 && GameLevel%2 == 1)
            {
                GameSpeed += 2;
            }
            ButtonText = "Click For Next Level";
            MainLoopCompleted?.Invoke(this, EventArgs.Empty);
            IsRunning = false;
        }

        public void GameOver()
        {
            Player.Color = "red";
            GameTimer.Dispose();
            BotTimer.Dispose();
            ButtonText = "Click To Start";
            GameLevel = 0;
            GameScore = 0;
            GameSpeed = 5;
            MainLoopCompleted?.Invoke(this, EventArgs.Empty);
            IsRunning = false;

        }
    }

}
