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
            if (DistanceFromGround <= 0 || DistanceFromGround >= 475 || DistanceFromLeft <= 0 || DistanceFromLeft >= 574)
                return true;
            else
                return false;
            
        }
    }
}
