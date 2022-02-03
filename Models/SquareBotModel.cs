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

        public int Speed { get; private set; } = 15;

        private bool IsLeft { get; set; }

        private bool IsBottom { get; set; }

        public SquareBotModel()
        {
            GetPosition();
        }

        private void StartingPosition()
        {
            int x = new Random().Next(0, 2);
            int y = new Random().Next(0, 2);

            if (x == 0)
                IsLeft = true;
            else
                IsLeft = false;

            if (y == 0)
                IsBottom = true;
            else
                IsBottom = false;
        }


        public void GetPosition()
        {
            StartingPosition();

            if (IsLeft)
                FromLeft = new Random().Next(-30, 0);
            else
                FromLeft = new Random().Next(600, 650);

            if (IsBottom)
                FromGround = new Random().Next(500, 550);
            else
                FromGround = new Random().Next(50, 100);
        }
    }
}
