using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game.Models
{
    public class SquareBotModel
    {
        public int FromGround { get; private set; }

        public int FromLeft { get; private set; }

        public string Color { get; set; } = "black";

        public int Speed { get; private set; } = 5;

        public string Direction { get; set; }

        public SquareBotModel()
        {
            GetPosition();
        }

        public void GetPosition() {
            
            int pick = new Random().Next(0, 4);

             switch (pick)
            {
                case 0: //left side
                    FromLeft = -40;
                    FromGround = new Random().Next(10, 460);
                    Direction = "right";
                    break;
                case 1: //top
                    FromLeft = new Random().Next(0, 560);
                    FromGround = 500;
                    Direction = "down";
                    break;
                case 2: //right side
                    FromLeft = 610;
                    FromGround = new Random().Next(10, 460);
                    Direction = "left";
                    break;
                case 3: //bottom
                    FromLeft = new Random().Next(0, 560);
                    FromGround = -40;
                    Direction = "up";
                    break;
            }
        }
         
        public void Move()
        {
             if (Direction == "right")
                FromLeft += Speed;
            if (Direction == "left")
                FromLeft -= Speed;
            if (Direction == "up")
                FromGround += Speed;
            if (Direction == "down")
                FromGround -= Speed;
        }

        public bool IsOffScreen()
        {
            if (FromLeft <= -50 || FromLeft >= 650)
                return true;
            if (FromGround <= -50 || FromGround >= 550)
                return true;
            else
                return false;
        }

       
    }
}
