using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game.Models
{
    public class PlayerModel
    {
        public int DistanceFromGround { get; private set; } = 237;

        public int DistanceFromLeft { get; private set; } = 287;

        public string Color { get; set; } = "red";

        public int Speed { get; private set; } = 20;

        public void MoveUp()
        {
            DistanceFromGround += Speed;
        }

        public void MoveDown()
        {
            DistanceFromGround -= Speed;
        }

        public void MoveLeft()
        {
            DistanceFromLeft -= Speed;
        }

        public void MoveRight()
        {
            DistanceFromLeft += Speed;
        }

        public bool HitWall()
        {
            if (DistanceFromGround <= 8 || DistanceFromGround >= 450 || DistanceFromLeft <= 13 || DistanceFromLeft >= 574)
                return true;
            else
                return false;
            
        }
    }
}
