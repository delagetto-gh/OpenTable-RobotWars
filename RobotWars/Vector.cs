using System.Collections.Generic;

namespace RobotWars
{
    public struct Vector
    {
        public Vector(int x, int y, CardinalPoint direction)
        {
            this.X = x;
            this.Y = y;
            this.Direction = direction;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public CardinalPoint Direction { get; private set; }
    }
}